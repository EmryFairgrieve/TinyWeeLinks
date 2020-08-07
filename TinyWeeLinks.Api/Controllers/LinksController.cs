using Microsoft.AspNetCore.Mvc;
using TinyWeeLinks.Api.Services;
using TinyWeeLinks.Api.Data;

namespace TinyWeeLinks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinksController : ControllerBase
    {
        private readonly ILinkService _linkService;

        public LinksController(ILinkService linkService) => _linkService = linkService;

        [HttpGet]
        public IActionResult Get(string shortcut, string twlSecret)
        {
            var link = _linkService.FindLink(shortcut, twlSecret);
            return Ok(link);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Link link) 
        {
            var createdLink = _linkService.CreateLink(link.Url);
            return Ok(createdLink);
        }
    }
}
