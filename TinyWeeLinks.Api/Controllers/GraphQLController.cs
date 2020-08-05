using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using TinyWeeLinks.Api.Models;
using TinyWeeLinks.Api.Services;
using TinyWeeLinks.Api.Schemas;
using TinyWeeLinks.Api.Queries;
using TinyWeeLinks.Api.Mutations;

namespace TinyWeeLinks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraphQLController : ControllerBase
    {
        private readonly ILinkService _linkService;

        public GraphQLController(ILinkService linkService) => _linkService = linkService;

        [HttpPost]
        public async Task<IActionResult> Post(GraphQLQuery query)
        {
            var schema = new Schema()
            {
                Query = new LinkQuery(_linkService),
                Mutation = new LinkMutation(_linkService)
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
