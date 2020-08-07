using GraphQL.Types;
using TinyWeeLinks.Api.Data;

namespace TinyWeeLinks.Api.Schemas
{
    public class LinkType : ObjectGraphType<Link>
    {
        public LinkType()
        {
            Name = "LinkType";
            Description = "Link information";

            Field(x => x.Shortcut).Description("The shortened URL of the Link.");
            Field(x => x.Url).Description("The full URL the Link redirects to.");
            Field(x => x.DateTimeCreated).Description("The expiry date of the Link.");
            Field(x => x.Secret).Description("The secret code required to view Link statistics.");
        }
    }
}
