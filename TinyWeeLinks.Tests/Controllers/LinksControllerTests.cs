using System;
using Moq;
using TinyWeeLinks.Api.Controllers;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Services;
using Xunit;

namespace TinyWeeLinks.Tests.Controllers
{
    public class LinksControllerTests
    {
        private readonly LinksController _linksController;
        private readonly Mock<ILinkService> _linkService;

        public LinksControllerTests()
        {
            _linkService = new Mock<ILinkService>(MockBehavior.Strict);
            _linksController = new LinksController(_linkService.Object);
        }

        [Fact]
        public void Get_LinkSupplied_CallsService()
        {
            var shortcut = "shortcut";
            var secret = "secret";
            var link = new Link { Shortcut = shortcut, Secret = secret, Id = 5 };
            _linkService.Setup(l => l.FindLink(shortcut, secret)).Returns(link);

            _linksController.Get(shortcut, secret);

            _linkService.Verify(l => l.FindLink(shortcut, secret), Times.Once());
        }

        [Fact]
        public void Post_LinkSupplied_CallsService()
        {
            var shortcut = "shortcut";
            var url = "url";
            var link = new Link { Shortcut = shortcut, Url = url, Id = 3 };
            _linkService.Setup(l => l.CreateLink(url)).Returns(link);

            _linksController.Post(link);

            _linkService.Verify(l => l.CreateLink(url), Times.Once());
        }
    }
}
