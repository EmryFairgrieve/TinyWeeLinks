using Moq;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Models;
using TinyWeeLinks.Api.Repositories;
using TinyWeeLinks.Api.Services;
using Xunit;

namespace TinyWeeLinks.Tests.Services
{
    public class ClickServiceTests
    {
        private readonly IClickService _clickService;
        private readonly Mock<IClickRepository> _clickRepository;
        private readonly Mock<ILinkService> _linkService;

        public ClickServiceTests()
        {
            _clickRepository = new Mock<IClickRepository>(MockBehavior.Strict);
            _linkService = new Mock<ILinkService>(MockBehavior.Strict);
            
            _clickService = new ClickService(_linkService.Object, _clickRepository.Object);
        }

        [Fact]
        public void TrackClick_ValidShortcut_TracksClick()
        {
            var validShortcut = "shortcut";
            var linkId = 2;
            var link = new Link { Shortcut = validShortcut, Id = linkId };
            _linkService.Setup(l => l.FindLinkByShortcut(validShortcut)).Returns(new Result<Link>(200) { Data = link } );
            _clickRepository.Setup(c => c.Create(It.IsAny<Click>())).Returns(true);

            var result = _clickService.TrackClick(validShortcut);

            _linkService.Verify(l => l.FindLinkByShortcut(validShortcut), Times.Exactly(2));
            _clickRepository.Verify(c => c.Create(It.IsAny<Click>()), Times.Once);
            Assert.Equal(link, result.Data);
        }

        [Fact]
        public void TrackClick_InvalidShortcut_DoesntTrackClick()
        {
            var invalidShortcut = "invalidshortcut";
            _linkService.Setup(l => l.FindLinkByShortcut(invalidShortcut)).Returns(new Result<Link>(400) { ErrorMessage = "Could not find link with shortcut invalidshortcut" });

            var result = _clickService.TrackClick(invalidShortcut);

            _linkService.Verify(l => l.FindLinkByShortcut(invalidShortcut), Times.Once);
            _clickRepository.Verify(c => c.Create(It.IsAny<Click>()), Times.Never);
            Assert.Null(result.Data);
            Assert.Equal("Could not find link with shortcut invalidshortcut", result.ErrorMessage);
        }

        [Fact]
        public void TrackClick_Null_DoesntTrackClick()
        {
            string invalidShortcut = null;
            _linkService.Setup(l => l.FindLinkByShortcut(invalidShortcut)).Returns(new Result<Link>(400) { ErrorMessage = "No shortcut was supplied in the URL" });

            var result = _clickService.TrackClick(invalidShortcut);

            _linkService.Verify(l => l.FindLinkByShortcut(invalidShortcut), Times.Once);
            _clickRepository.Verify(c => c.Create(It.IsAny<Click>()), Times.Never);
            Assert.Null(result.Data);
            Assert.Equal("No shortcut was supplied in the URL", result.ErrorMessage);
        }

        [Fact]
        public void TrackClick_DatabaseErrorCreatingClick_DoesntTrackClick()
        {
            var validShortcut = "shortcut";
            var linkId = 2;
            var link = new Link { Shortcut = validShortcut, Id = linkId };
            _linkService.Setup(l => l.FindLinkByShortcut(validShortcut)).Returns(new Result<Link>(200) { Data = link });
            _clickRepository.Setup(c => c.Create(It.IsAny<Click>())).Returns(false);

            var result = _clickService.TrackClick(validShortcut);

            _linkService.Verify(l => l.FindLinkByShortcut(validShortcut), Times.Once);
            _clickRepository.Verify(c => c.Create(It.IsAny<Click>()), Times.Once);
            Assert.Null(result.Data);
            Assert.Equal("Unable to track click", result.ErrorMessage);
        }
    }
}
