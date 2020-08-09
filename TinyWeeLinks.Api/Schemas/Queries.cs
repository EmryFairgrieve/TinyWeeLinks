using GraphQL;
using GraphQL.Types;
using TinyWeeLinks.Api.Schemas;
using TinyWeeLinks.Api.Services;

namespace TinyWeeLinks.Api.Schemas
{
    public class Queries : ObjectGraphType
    {
        public Queries(ILinkService linkService)
        {
            Name = "Query";
            Description = "This is where the queries are.";

            Field<LinkInfoType>("link",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "shortcut", Description = "The shortened URL of the link." },
                    new QueryArgument<StringGraphType> { Name = "twlSecret", Description = "The secret for accessing statics about the link." }
                ),
                description: "Information about current link", resolve: context =>
            {
                var shortcut = context.GetArgument<string>("shortcut");
                var secret = context.GetArgument<string>("twlSecret");

                var result = linkService.FindLink(shortcut, secret);
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    context.Errors.Add(new ExecutionError(result.ErrorMessage));
                    return null;
                }
                return result.Data;
            });

            Field< ListGraphType<LinkType>>("links",
                description: "List of availble links", resolve: context =>
                {
                    var result = linkService.GetLinks();
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        context.Errors.Add(new ExecutionError(result.ErrorMessage));
                        return null;
                    }
                    return result.Data;
                });
        }
    }
}
