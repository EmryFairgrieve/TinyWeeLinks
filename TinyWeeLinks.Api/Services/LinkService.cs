using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Models;
using TinyWeeLinks.Api.Repositories;

namespace TinyWeeLinks.Api.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;

        public LinkService(ILinkRepository linkRepository) => _linkRepository = linkRepository;

        public Result<LinkInfo> CreateLink(string url)
        {
            var result = new Result<LinkInfo>(201);

            if (string.IsNullOrEmpty(url))
            {
                result.Status = 400;
                result.ErrorMessage = "No URL was supplied";
                return result;
            }
            url = url.StartsWith("http") ? url: "https://" + url;
            if (!IsLinkValid(url))
            {
                result.Status = 400;
                result.ErrorMessage = "The URL supplied was not accessible";
                return result;
            }

            var secret = Guid.NewGuid().ToString();
            var shortcut = secret.Substring(0, 8);

            var link = new Link { DateTimeCreated = DateTime.UtcNow, Secret = Guid.NewGuid().ToString(), Url = url, Shortcut = shortcut };
            var success = _linkRepository.Create(link);

            if (!success)
            {
                result.Status = 500;
                result.ErrorMessage = "Unable to create new link";
            }
            else
            {
                result.Data = ConvertToLinkInfo(link);
            }
            return result;
        }

        public Result<LinkInfo> FindLink(string shortcut, string secret)
        {
            var result = new Result<LinkInfo>(200);
            if (string.IsNullOrEmpty(shortcut))
            {
                result.Status = 400;
                result.ErrorMessage = "No shortcut was supplied in the URL";
                return result;
            }
            var link = _linkRepository.FindByShortcut(shortcut);
            if (link == null)
            {
                result.Status = 400;
                result.ErrorMessage = "Could not find link with shortcut " + shortcut;
            }
            else if (link?.Secret != secret)
            {
                result.Status = 400;
                result.ErrorMessage = "Shortcut and secret supplied in the URL do not match";
            }
            else
            {
                result.Data = ConvertToLinkInfo(link);
            }
            return result;
        }

        public Result<Link> FindLinkByShortcut(string shortcut)
        {
            var result = new Result<Link>(200);
            if (string.IsNullOrEmpty(shortcut))
            {
                result.Status = 400;
                result.ErrorMessage = "No shortcut was supplied in the URL";
                return result;
            }
            var link = _linkRepository.FindByShortcut(shortcut);
            if (link == null)
            {
                result.Status = 400;
                result.ErrorMessage = "Could not find link with shortcut " + shortcut;
            }
            else
            {
                result.Data = link;
            }
            return result;
        }

        public Result<ICollection<Link>> GetLinks()
        {
            var result = new Result<ICollection<Link>>(200);
            var links = _linkRepository.GetLinks();
            if (links == null)
            {
                result.Status = 500;
                result.ErrorMessage = "Unable to find links";
            }
            else
            {
                result.Data = links;
            }
            return result;
        }

        private LinkInfo ConvertToLinkInfo(Link link)
        {
            return new LinkInfo
            {
                Chart = AddStatics(link),
                DateTimeCreated = link.DateTimeCreated,
                Shortcut = link.Shortcut,
                TotalClicks = link.Clicks != null ? link.Clicks.Count : 0,
                TwlSecret = link.Secret,
                Url = link.Url
            };
        }

        private Chart AddStatics(Link link)
        {
            var dates = Enumerable.Range(0, 7)
                          .Select(offset => DateTime.Today.AddDays(-1 * offset))
                          .Reverse()
                          .ToList();

            var labels = dates.Select(date => date.ToString("ddd d").ToLower()).ToList();
            var values = Enumerable.Range(0, 7).Select(i => 0).ToList();

            if (link.Clicks != null) {
                values = Enumerable.Range(0, 7)
                    .Select(offset => link.Clicks.Count(x => x.DateTimeClicked.Date == dates[offset]))
                    .ToList();
            }

            return new Chart
            {
                Title = "clicks per day",
                Labels = labels,
                Values = values
            };
        }

        private bool IsLinkValid(string url)
        {
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }
    }
}
