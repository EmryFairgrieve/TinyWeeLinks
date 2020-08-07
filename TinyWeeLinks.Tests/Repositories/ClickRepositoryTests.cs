using System;
using Moq;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Repositories;
using Xunit;

namespace TinyWeeClicks.Tests.Repositories
{
    public class ClickRepositoryTests
    {
        private readonly IClickRepository _clickRepository;
        private readonly Mock<IApplicationDbContext> _applicationDbContext;

        public ClickRepositoryTests()
        {
            _applicationDbContext = new Mock<IApplicationDbContext>(MockBehavior.Strict);
            _clickRepository = new ClickRepository(_applicationDbContext.Object);
        }

        [Fact]
        public void Create_ValidClickSupplied_CreatesClick()
        {
            var validLink = new Link
            {
                Id = 4,
                Url = "https://test.com/"
            };

            var validClick = new Click
            {
                Id = 1,
                DateTimeClicked = DateTime.UtcNow,
            };
            _applicationDbContext.Setup(a => a.AddClick(validClick));
            _applicationDbContext.Setup(a => a.SaveChanges()).Returns(1);

            var result = _clickRepository.Create(validClick);

            Assert.True(result);
        }

        [Fact]
        public void Create_ClickDoesntHaveALinkSupplied_DoesntCreateClick()
        {
            var validClick = new Click
            {
                Id = 1,
                DateTimeClicked = DateTime.UtcNow
            };
            _applicationDbContext.Setup(a => a.AddClick(validClick));
            _applicationDbContext.Setup(a => a.SaveChanges()).Returns(0);

            var result = _clickRepository.Create(validClick);

            Assert.False(result);
        }

        [Fact]
        public void Create_InValidClickSupplied_DoesntCreateClick()
        {
            Click validClick = null;
            _applicationDbContext.Setup(a => a.AddClick(validClick));
            _applicationDbContext.Setup(a => a.SaveChanges()).Returns(0);

            var result = _clickRepository.Create(validClick);

            Assert.False(result);
        }
    }
}
