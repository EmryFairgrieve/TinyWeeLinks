using System;
using System.Collections.Generic;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Models;

namespace TinyWeeLinks.Api.Services
{
    public interface ILinkService
    {
        LinkInfo CreateLink(string url);
        LinkInfo FindLink(string shortcut, string secret);
        Link FindLinkByShortcut(string shortcut);
        ICollection<Link> GetLinks();
    }
}
