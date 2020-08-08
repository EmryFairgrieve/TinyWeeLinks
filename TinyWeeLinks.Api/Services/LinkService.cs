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

        public LinkInfo CreateLink(string url)
        {
            url = url.StartsWith("http") ? url: "http://" + url;
            if (!IsLinkValid(url))
            {
                return null;
            }

            var secret = Guid.NewGuid().ToString();
            var shortcut = secret.Substring(0, 8);

            var link = new Link { DateTimeCreated = DateTime.UtcNow, Secret = Guid.NewGuid().ToString(), Url = url, Shortcut = shortcut };
            var success = _linkRepository.Create(link);

            return success ? ConvertToLinkInfo(link) : null;
        }

        public LinkInfo FindLink(string shortcut, string secret)
        {
            var link = _linkRepository.FindByShortcut(shortcut);
            return link?.Secret == secret ? ConvertToLinkInfo(link) : null;
        }

        public Link FindLinkByShortcut(string shortcut)
        {
            return _linkRepository.FindByShortcut(shortcut);
        }

        public ICollection<Link> GetLinks()
        {
            return _linkRepository.GetLinks();
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
