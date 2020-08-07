using System;
using System.Collections.Generic;
using System.Net;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Repositories;

namespace TinyWeeLinks.Api.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;

        public LinkService(ILinkRepository linkRepository) => _linkRepository = linkRepository;

        public bool AddClickToLink(string shortcut, Click click)
        {
            var link = _linkRepository.FindByShortcut(shortcut);
            var success = link != null;
            if (success)
            {
                link.Clicks ??= new List<Click>();
                link.Clicks.Add(click);
                success = _linkRepository.Update(link);
            }
            return success;
        }

        public Link CreateLink(string url)
        {
            if (!IsLinkValid(url))
            {
                return null;
            }

            var secret = Guid.NewGuid().ToString();
            var shortcut = secret.Substring(0, 8);

            var link = new Link { DateTimeCreated = DateTime.UtcNow, Secret = Guid.NewGuid().ToString(), Url = url, Shortcut = shortcut };
            var success = _linkRepository.Create(link);

            return success ? link : null;
        }

        public Link FindLink(string shortcut, string secret)
        {
            var link = _linkRepository.FindByShortcut(shortcut);
            return link?.Secret == secret ? link : null;
        }

        public Link FindLinkByShortcut(string shortcut)
        {
            return _linkRepository.FindByShortcut(shortcut);
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
