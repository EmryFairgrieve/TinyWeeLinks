using System;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Models;
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

        public Result<Link> TrackClick(string shortcut)
        {
            var linkResult = _linkService.FindLinkByShortcut(shortcut);
            if (linkResult.Status != 200)
            {
                return linkResult;
            }
            var click = new Click { DateTimeClicked = DateTime.UtcNow, LinkId = linkResult.Data.Id };
            var success = _clickRepository.Create(click);
            if (!success)
            {
                return new Result<Link>(500) { ErrorMessage = "Unable to track click" };
            }
            else
            {
                return _linkService.FindLinkByShortcut(shortcut);
            }
        }
    }
}
