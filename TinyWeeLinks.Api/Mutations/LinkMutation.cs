using System;
using GraphQL.Types;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Schemas;
using TinyWeeLinks.Api.Services;

namespace TinyWeeLinks.Api.Mutations
{
    public class LinkMutation : ObjectGraphType
    {
        public LinkMutation(ILinkService linkService)
        {
            Name = "LinkMutation";

            Field<LinkType>("createLink",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "url", Description = "The URL of the link." }
                ),
                description: "Create a new link", resolve: context =>
                {
                    var url = context.GetArgument<string>("url");
                    return linkService.CreateLink(url);
                });
        }
    }
}
