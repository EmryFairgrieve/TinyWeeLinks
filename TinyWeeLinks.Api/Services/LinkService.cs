using System;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Repositories;

namespace TinyWeeLinks.Api.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;

        public LinkService(ILinkRepository linkRepository) => _linkRepository = linkRepository;

        public Link CreateLink(string url)
        {
            var secret = Guid.NewGuid().ToString();
            var shortcut = secret.Substring(0, 8);

            var link = new Link { ExpiryDate = DateTime.UtcNow, Secret = Guid.NewGuid().ToString(), Url = url, Shortcut = shortcut };
            var success = _linkRepository.Create(link);

            return success ? _linkRepository.FindByShortcut(shortcut) : null;
        }

        public Link FindLink(string shortcut, string secret)
        {
            var link = _linkRepository.FindByShortcut(shortcut);
            return link?.ExpiryDate <= DateTime.UtcNow && link?.Secret == secret ? link : new Link();
        }
    }
}
