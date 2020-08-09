using System;
using System.Collections.Generic;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Models;

namespace TinyWeeLinks.Api.Services
{
    public interface ILinkService
    {
        Result<LinkInfo> CreateLink(string url);
        Result<LinkInfo> FindLink(string shortcut, string secret);
        Result<Link> FindLinkByShortcut(string shortcut);
        Result<ICollection<Link>> GetLinks();
    }
}
