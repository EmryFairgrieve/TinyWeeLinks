﻿using GraphQL.Types;
using TinyWeeLinks.Api.Services;

namespace TinyWeeLinks.Api.Schemas
{
    public class Mutations : ObjectGraphType
    {
        public Mutations(ILinkService linkService, IClickService clickService)
        {
            Name = "Mutation";
            Description = "This is where the mutations are.";

            Field<LinkType>("createLink",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "url", Description = "The URL of the link." }
                ),
                description: "Create a new link", resolve: context =>
                {
                    var url = context.GetArgument<string>("url");
                    return linkService.CreateLink(url);
                });

            Field<LinkType>("trackClick",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "shortcut", Description = "The shortcut of the link." }
                ),
                description: "Track new click of a link", resolve: context =>
                {
                    var shortcut = context.GetArgument<string>("shortcut");
                    return clickService.TrackClick(shortcut);
                });
        }
    }
}