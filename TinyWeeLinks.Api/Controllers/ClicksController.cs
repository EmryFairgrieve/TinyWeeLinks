using System;
using Microsoft.AspNetCore.Mvc;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Models;
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
            return CorrectType(_clickService.TrackClick(link.Shortcut));
        }


        private IActionResult CorrectType(Result<Link> result)
        {
            if (result.Status == 400)
            {
                return BadRequest(result.ErrorMessage);
            }
            if (result.Status == 500)
            {
                return StatusCode(500);
            }
            return Ok(result.Data);
        }
    }
}
