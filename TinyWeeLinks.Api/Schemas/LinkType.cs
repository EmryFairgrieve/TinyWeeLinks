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

            Field(x => "").Description("The shortened URL of the Link.");
            Field(x => "").Description("The full URL the Link redirects to.");
            Field(x => x.ExpiryDate).Description("The expiry date of the Link.");
            Field(x => "").Description("The secret code required to view Link statistics.");
        }
    }
}
