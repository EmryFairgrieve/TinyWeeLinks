using System;
using System.Collections.Generic;

namespace TinyWeeLinks.Api.Models
{
    public class Chart
    {
        public string Title { get; set; }
        public ICollection<string> Labels { get; set; }
        public ICollection<int> Values { get; set; }
    }
}
