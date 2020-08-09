using System;
using GraphQL.Types;
using TinyWeeLinks.Api.Models;

namespace TinyWeeLinks.Api.Schemas
{
    public class ChartType : ObjectGraphType<Chart>
    {
        public ChartType()
        {
            Name = "ChartType";
            Description = "Chart information";

            Field(x => x.Title).Description("The title of the Chart.");
            Field(x => x.Labels).Description("The labels for the Chart data.");
            Field(x => x.Values).Description("The values of the Chart data.");
        }
    }
}
