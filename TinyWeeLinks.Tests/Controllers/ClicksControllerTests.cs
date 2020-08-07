using System;
using Moq;
using TinyWeeLinks.Api.Controllers;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Services;
using Xunit;

namespace TinyWeeLinks.Tests.Controllers
{
    public class ClicksControllerTests
    {
        private readonly ClicksController _clicksController;
        private readonly Mock<IClickService> _clickService;

        public ClicksControllerTests()
        {
            _clickService = new Mock<IClickService>(MockBehavior.Strict);
            _clicksController = new ClicksController(_clickService.Object);
        }

        [Fact]
        public void Post_LinkSupplied_CallsService()
        {
            var link = new Link { Shortcut = "shortcut" };
            _clickService.Setup(c => c.TrackClick(link.Shortcut)).Returns(link);

            _clicksController.Post(link);

            _clickService.Verify(c => c.TrackClick(link.Shortcut), Times.Once());
        }
    }
}
