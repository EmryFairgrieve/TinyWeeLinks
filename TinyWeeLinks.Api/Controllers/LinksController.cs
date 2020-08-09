using Microsoft.AspNetCore.Mvc;
using TinyWeeLinks.Api.Services;
using TinyWeeLinks.Api.Data;
using Microsoft.AspNetCore.Http;
using TinyWeeLinks.Api.Models;
using System.Collections.Generic;

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
            if (shortcut == null && twlSecret == null)
            {
                return CorrectType(_linkService.GetLinks());
            }
            return CorrectType(_linkService.FindLink(shortcut, twlSecret));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Link link) 
        {
            return CorrectType(_linkService.CreateLink(link.Url));
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

        private IActionResult CorrectType(Result<LinkInfo> result)
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

        private IActionResult CorrectType(Result<ICollection<Link>> result)
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
