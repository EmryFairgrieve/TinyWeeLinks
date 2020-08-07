using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using TinyWeeLinks.Api.Models;
using TinyWeeLinks.Api.Schemas;
using TinyWeeLinks.Api.Services;

namespace TinyWeeLinks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraphQLController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly IClickService _clickService;

        public GraphQLController(ILinkService linkService, IClickService clickService)
        {
            _linkService = linkService;
            _clickService = clickService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(GraphQLQuery query)
        {
            var schema = new Schema()
            {
                Query = new Queries(_linkService),
                Mutation = new Mutations(_linkService, _clickService)
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.Inputs = query.variables;
                _.OperationName = query.operationName;
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(new { result.Data });
        }
    }
}
