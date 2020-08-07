using Moq;
using TinyWeeLinks.Api.Repositories;
using TinyWeeLinks.Api.Services;

namespace TinyWeeLinks.Tests.Services
{
    public class LinkServiceTests
    {
        private readonly ILinkService _linkService;
        private readonly Mock<ILinkRepository> _linkRepository;

        public LinkServiceTests()
        {
            _linkRepository = new Mock<ILinkRepository>(MockBehavior.Strict);
            _linkService = new LinkService(_linkRepository.Object);
        }

        //Link CreateLink(string url);
        //link works
        //link doesn't work
        //link isn't the right format
        //link is empty
        //link is null

        //Link FindLink(string shortcut, string secret);
        //shortcut is there, secret empty
        //shortcut and secret given
        //shortcut given, wrong secret
        //shortcut is empty
        //shortcut is null
    }
}
