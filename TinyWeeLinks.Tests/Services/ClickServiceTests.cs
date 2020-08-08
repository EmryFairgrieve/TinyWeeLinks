using Moq;
using TinyWeeLinks.Api.Data;
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
            _linkService.Setup(l => l.FindLinkByShortcut(validShortcut)).Returns(link);
            _clickRepository.Setup(c => c.Create(It.IsAny<Click>())).Returns(true);

            var response = _clickService.TrackClick(validShortcut);

            _linkService.Verify(l => l.FindLinkByShortcut(validShortcut), Times.Exactly(2));
            _clickRepository.Verify(c => c.Create(It.IsAny<Click>()), Times.Once);
            Assert.Equal(link, response);
        }

        [Fact]
        public void TrackClick_InvalidShortcut_DoesntTrackClick()
        {
            var invalidShortcut = "invalid";
            _linkService.Setup(l => l.FindLinkByShortcut(invalidShortcut)).Returns((Link) null);

            var response = _clickService.TrackClick(invalidShortcut);

            _linkService.Verify(l => l.FindLinkByShortcut(invalidShortcut), Times.Once);
            _clickRepository.Verify(c => c.Create(It.IsAny<Click>()), Times.Never);
            Assert.Equal((Link) null, response);
        }

        [Fact]
        public void TrackClick_Null_DoesntTrackClick()
        {
            string invalidShortcut = null;
            _linkService.Setup(l => l.FindLinkByShortcut(invalidShortcut)).Returns((Link)null);

            var response = _clickService.TrackClick(invalidShortcut);

            _linkService.Verify(l => l.FindLinkByShortcut(invalidShortcut), Times.Once);
            _clickRepository.Verify(c => c.Create(It.IsAny<Click>()), Times.Never);
            Assert.Equal((Link) null, response);
        }

        [Fact]
        public void TrackClick_DatabaseErrorCreatingClick_DoesntTrackClick()
        {
            var validShortcut = "shortcut";
            var linkId = 2;
            var link = new Link { Shortcut = validShortcut, Id = linkId };
            _linkService.Setup(l => l.FindLinkByShortcut(validShortcut)).Returns(link);
            _clickRepository.Setup(c => c.Create(It.IsAny<Click>())).Returns(false);

            var response = _clickService.TrackClick(validShortcut);

            _linkService.Verify(l => l.FindLinkByShortcut(validShortcut), Times.Once);
            _clickRepository.Verify(c => c.Create(It.IsAny<Click>()), Times.Once);
            Assert.Equal((Link) null, response);
        }
    }
}
