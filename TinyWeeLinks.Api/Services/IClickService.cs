using System;
using TinyWeeLinks.Api.Data;
using TinyWeeLinks.Api.Models;

namespace TinyWeeLinks.Api.Services
{
    public interface IClickService
    {
        Result<Link> TrackClick(string shortcut);
    }
}
