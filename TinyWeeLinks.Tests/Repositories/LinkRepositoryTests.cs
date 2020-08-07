using Moq;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Repositories;
using Xunit;

namespace TinyWeeLinks.Tests.Repositories
{
    public class LinkRepositoryTests
    {
        private readonly ILinkRepository _linkRepository;
        private readonly Mock<IApplicationDbContext> _applicationDbContext;

        public LinkRepositoryTests()
        {
            _applicationDbContext = new Mock<IApplicationDbContext>(MockBehavior.Strict);
            _linkRepository = new LinkRepository(_applicationDbContext.Object);
        }

        [Fact]
        public void FindByShortcut_ValidShortcutSupplied_ReturnsCorrespondingLink()
        {
            var availableShortcut = "qwerty";
            var link = new Link { Id = 1, Secret = "secret", Url = "https://long.url/qwerty", Shortcut = availableShortcut };
            _applicationDbContext.Setup(a => a.GetLink(availableShortcut)).Returns(link);

            var result = _linkRepository.FindByShortcut(availableShortcut);

            _applicationDbContext.Verify(a => a.GetLink(availableShortcut), Times.Once);
            Assert.Equal(availableShortcut, result.Shortcut);
            Assert.Equal(link, result);
        }

        [Fact]
        public void FindByShortcut_InValidShortcutSupplied_ReturnsNull()
        {
            var availableShortcut = "";
            _applicationDbContext.Setup(a => a.GetLink(availableShortcut)).Returns((Link) null);

            var result = _linkRepository.FindByShortcut(availableShortcut);

            _applicationDbContext.Verify(a => a.GetLink(availableShortcut), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public void FindByShortcut_NullShortcutSupplied_ReturnsNull()
        {
            string availableShortcut = null;
            _applicationDbContext.Setup(a => a.GetLink(availableShortcut)).Returns((Link)null);

            var result = _linkRepository.FindByShortcut(availableShortcut);

            _applicationDbContext.Verify(a => a.GetLink(availableShortcut), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public void Create_ValidLinkSupplied_CreatesLink()
        {
            var validLink = new Link
            {
                Id = 4,
                Url = "https://test.com/"
            };
            _applicationDbContext.Setup(a => a.AddLink(validLink));
            _applicationDbContext.Setup(a => a.SaveChanges()).Returns(1);

            var result = _linkRepository.Create(validLink);

            _applicationDbContext.Verify(a => a.AddLink(validLink), Times.Once);
            _applicationDbContext.Verify(a => a.SaveChanges(), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void Create_InValidLinkSupplied_DoesntCreateLink()
        {
            Link validLink = null;
            _applicationDbContext.Setup(a => a.AddLink(validLink));
            _applicationDbContext.Setup(a => a.SaveChanges()).Returns(0);

            var result = _linkRepository.Create(validLink);

            _applicationDbContext.Verify(a => a.AddLink(validLink), Times.Once);
            _applicationDbContext.Verify(a => a.SaveChanges(), Times.Once);
            Assert.False(result);
        }

        [Fact]
        public void Update_ValidLinkSupplied_UpdatesLink()
        {
            var validLink = new Link
            {
                Id = 4,
                Url = "https://test.com/"
            };
            _applicationDbContext.Setup(a => a.UpdateLink(validLink));
            _applicationDbContext.Setup(a => a.SaveChanges()).Returns(1);

            var result = _linkRepository.Update(validLink);

            _applicationDbContext.Verify(a => a.UpdateLink(validLink), Times.Once);
            _applicationDbContext.Verify(a => a.SaveChanges(), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void Update_InValidLinkSupplied_DoesntUpdateLink()
        {
            Link validLink = null;
            _applicationDbContext.Setup(a => a.UpdateLink(validLink));
            _applicationDbContext.Setup(a => a.SaveChanges()).Returns(0);

            var result = _linkRepository.Update(validLink);

            _applicationDbContext.Verify(a => a.UpdateLink(validLink), Times.Once);
            _applicationDbContext.Verify(a => a.SaveChanges(), Times.Once);
            Assert.False(result);
        }
    }
}
