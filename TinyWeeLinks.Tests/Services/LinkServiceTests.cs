using System.Collections.Generic;
using Moq;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Repositories;
using TinyWeeLinks.Api.Services;
using Xunit;

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

        [Fact]
        public void CreateLink_ValidAccessibleUrl_LinkCreated()
        {
            var url = "https://www.google.com/";
            _linkRepository.Setup(l => l.Create(It.IsAny<Link>())).Returns(true);

            var result = _linkService.CreateLink(url);

            Assert.Equal(url, result.Url);
            _linkRepository.Verify(l => l.Create(It.IsAny<Link>()), Times.Once);
        }

        [Fact]
        public void CreateLink_AnotherValidAccessibleUrl_LinkCreated()
        {
            var url = "google.com";
            _linkRepository.Setup(l => l.Create(It.IsAny<Link>())).Returns(true);

            var result = _linkService.CreateLink(url);

            Assert.Equal(url, result.Url);
            _linkRepository.Verify(l => l.Create(It.IsAny<Link>()), Times.Once);
        }

        [Fact]
        public void CreateLink_InvalidUrl_LinkNotCreated()
        {
            var url = "invalidUrl";

            var result = _linkService.CreateLink(url);

            Assert.Null(result);
            _linkRepository.Verify(l => l.Create(It.IsAny<Link>()), Times.Never);
        }

        [Fact]
        public void CreateLink_InaccessibleUrl_LinkNotCreated()
        {
            var url = "inaccessibleUrl";

            var result = _linkService.CreateLink(url);

            Assert.Null(result);
            _linkRepository.Verify(l => l.Create(It.IsAny<Link>()), Times.Never);
        }

        [Fact]
        public void CreateLink_NullUrl_LinkNotCreated()
        {
            string url = null;

            var result = _linkService.CreateLink(url);

            Assert.Null(result);
            _linkRepository.Verify(l => l.Create(It.IsAny<Link>()), Times.Never);
        }

        [Fact]
        public void CreateLink_DatabaseErrorOnCreate_LinkNotCreated()
        {
            var url = "https://www.google.com/";
            _linkRepository.Setup(l => l.Create(It.IsAny<Link>())).Returns(false);

            var result = _linkService.CreateLink(url);

            Assert.Null(result);
            _linkRepository.Verify(l => l.Create(It.IsAny<Link>()), Times.Once);
        }

        [Fact]
        public void FindLink_ValidShortcutAndSecret_ReturnsLink()
        {
            var shortcut = "validshortcut";
            var secret = "validsecret";
            _linkRepository.Setup(l => l.FindByShortcut(shortcut)).Returns(new Link { Shortcut = shortcut, Secret = secret });

            var result = _linkService.FindLink(shortcut, secret);

            Assert.Equal(shortcut, result.Shortcut);
            _linkRepository.Verify(l => l.FindByShortcut(shortcut), Times.Once);
        }

        [Fact]
        public void FindLink_InvalidShortcut_ReturnsNull()
        {
            var shortcut = "invalidshortcut";
            var secret = "validsecret";
            _linkRepository.Setup(l => l.FindByShortcut(shortcut)).Returns((Link) null);

            var result = _linkService.FindLink(shortcut, secret);

            Assert.Null(result);
            _linkRepository.Verify(l => l.FindByShortcut(shortcut), Times.Once);
        }

        [Fact]
        public void FindLink_NullShortcut_ReturnsNull()
        {
            string shortcut = null;
            var secret = "validsecret";
            _linkRepository.Setup(l => l.FindByShortcut(shortcut)).Returns((Link)null);

            var result = _linkService.FindLink(shortcut, secret);

            Assert.Null(result);
            _linkRepository.Verify(l => l.FindByShortcut(shortcut), Times.Once);
        }

        [Fact]
        public void FindLink_InvalidSecret_ReturnsNull()
        {
            var shortcut = "validshortcut";
            var secret = "invalidsecret";
            _linkRepository.Setup(l => l.FindByShortcut(shortcut)).Returns(new Link { Shortcut = shortcut, Secret = "validsecret" });

            var result = _linkService.FindLink(shortcut, secret);

            Assert.Null(result);
            _linkRepository.Verify(l => l.FindByShortcut(shortcut), Times.Once);
        }

        [Fact]
        public void FindLink_NullSecret_ReturnsNull()
        {
            var shortcut = "validshortcut";
            string secret = null;
            _linkRepository.Setup(l => l.FindByShortcut(shortcut)).Returns(new Link { Shortcut = shortcut, Secret = "validsecret" });

            var result = _linkService.FindLink(shortcut, secret);

            Assert.Null(result);
            _linkRepository.Verify(l => l.FindByShortcut(shortcut), Times.Once);
        }

        [Fact]
        public void FindLinkByShortcut_ValidShortcut_ReturnsLink()
        {
            var shortcut = "validshortcut";
            _linkRepository.Setup(l => l.FindByShortcut(shortcut)).Returns(new Link { Shortcut = shortcut });

            var result = _linkService.FindLinkByShortcut(shortcut);

            Assert.Equal(shortcut, result.Shortcut);
            _linkRepository.Verify(l => l.FindByShortcut(shortcut), Times.Once);
        }

        [Fact]
        public void FindLinkByShortcut_InvalidShortcut_ReturnsNull()
        {
            var shortcut = "invalidshortcut";
            _linkRepository.Setup(l => l.FindByShortcut(shortcut)).Returns((Link) null);

            var result = _linkService.FindLinkByShortcut(shortcut);

            Assert.Null(result);
            _linkRepository.Verify(l => l.FindByShortcut(shortcut), Times.Once);
        }

        [Fact]
        public void FindLinkByShortcut_NullShortcut_ReturnsNull()
        {
            string shortcut = null;
            _linkRepository.Setup(l => l.FindByShortcut(shortcut)).Returns((Link) null);

            var result = _linkService.FindLinkByShortcut(shortcut);

            Assert.Null(result);
            _linkRepository.Verify(l => l.FindByShortcut(shortcut), Times.Once);
        }
    }
}
