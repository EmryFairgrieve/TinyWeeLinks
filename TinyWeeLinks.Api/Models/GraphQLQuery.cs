using System;
using GraphQL;

namespace TinyWeeLinks.Api.Models
{
    public class GraphQLQuery
    {
        public string Query { get; set; }
        public Inputs variables { get; set; }
        public string operationName { get; set; }
    }
}
