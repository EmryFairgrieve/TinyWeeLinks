using System;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Repositories;

namespace TinyWeeLinks.Api.Services
{
    public class ClickService : IClickService
    {
        private readonly ILinkService _linkService;
        private readonly IClickRepository _clickRepository;

        public ClickService(ILinkService linkService, IClickRepository clickRepository)
        {
            _linkService = linkService;
            _clickRepository = clickRepository;
        }

        public Link TrackClick(string shortcut)
        {
            var link = _linkService.FindLinkByShortcut(shortcut);
            if (link == null)
            {
                return null;
            }
            var click = new Click { DateTimeClicked = DateTime.UtcNow, LinkId = link.Id };
            var success = _clickRepository.Create(click);
            return success ? _linkService.FindLinkByShortcut(shortcut) : null;
        }
    }
}
