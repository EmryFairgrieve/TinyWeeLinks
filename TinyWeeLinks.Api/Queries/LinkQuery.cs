using System;
using System.Collections.Generic;
using GraphQL.Types;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Schemas;
using TinyWeeLinks.Api.Services;

namespace TinyWeeLinks.Api.Queries
{
    public class LinkQuery : ObjectGraphType
    {
        public LinkQuery(ILinkService linkService)
        {
            Name = "LinkQuery";
            Description = "This is where the link queries are.";
            Field<LinkType>("link",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "shortcut", Description = "The shortened URL of the link." },
                    new QueryArgument<StringGraphType> { Name = "twlSecret", Description = "The secret for accessing statics about the link." }
                ),
                description: "Information about current link", resolve: context =>
            {
                var shortcut = context.GetArgument<string>("shortcut");
                var secret = context.GetArgument<string>("twlSecret");

                return linkService.FindLink(shortcut, secret);
            });
        }
    }
}
