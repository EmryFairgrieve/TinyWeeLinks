using System;
using TinyWeeLinks.Api.Data;

namespace TinyWeeLinks.Api.Models
{
    public class LinkInfo : Link
    {
        public string TwlSecret { get; set; }
        public int TotalClicks { get; set; }
        public Chart Chart { get; set; }
    }
}
