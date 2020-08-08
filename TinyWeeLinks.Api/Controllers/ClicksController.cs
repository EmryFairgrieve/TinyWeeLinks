using System;
using Microsoft.AspNetCore.Mvc;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Services;

namespace TinyWeeLinks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClicksController : ControllerBase
    {
        private readonly IClickService _clickService;

        public ClicksController(IClickService linkService) => _clickService = linkService;

        [HttpPost]
        public IActionResult Post([FromBody] Link link)
        {
            return Ok(_clickService.TrackClick(link.Shortcut));
        }
    }
}
