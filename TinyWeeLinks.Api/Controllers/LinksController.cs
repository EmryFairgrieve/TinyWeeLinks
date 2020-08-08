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
            return shortcut == null && twlSecret == null ? Ok(_linkService.GetLinks())
                                                            : Ok(_linkService.FindLink(shortcut, twlSecret));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Link link) 
        {
            return Ok(_linkService.CreateLink(link.Url));
        }
    }
}
