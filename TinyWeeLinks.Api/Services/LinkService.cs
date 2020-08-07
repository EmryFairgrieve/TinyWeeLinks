using System;
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

            return success ? _linkRepository.FindByShortcut(shortcut) : null;
        }

        public Link FindLink(string shortcut, string secret)
        {
            var link = _linkRepository.FindByShortcut(shortcut);
            return link?.DateTimeCreated <= DateTime.UtcNow && link?.Secret == secret ? link : new Link();
        }

        public Link FindLinkByShortcut(string shortcut)
        {
            var link = _linkRepository.FindByShortcut(shortcut);
            return link?.DateTimeCreated <= DateTime.UtcNow ? link : new Link();
        }

        private bool IsLinkValid(string url)
        {
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }
    }
}
