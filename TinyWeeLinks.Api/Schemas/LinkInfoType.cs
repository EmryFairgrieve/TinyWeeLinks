using System;
using GraphQL.Types;
using TinyWeeLinks.Api.Models;

namespace TinyWeeLinks.Api.Schemas
{
    public class LinkInfoType : ObjectGraphType<LinkInfo>
    {
        public LinkInfoType()
        {
            Name = "LinkInfoType";
            Description = "Full link information";

            Field(x => x.Shortcut).Description("The shortened URL of the Link.");
            Field(x => x.Url).Description("The full URL the Link redirects to.");
            Field(x => x.DateTimeCreated).Description("The date time the Link was created.");
            Field(x => x.TwlSecret).Description("The secret required to access full Link information.");
            Field(x => x.TotalClicks).Description("The total times a Link has been clicked");
            Field(x => x.Chart, type: typeof(ChartType)).Description("The information required to chart the activity of a Link.");
        }
    }
}
