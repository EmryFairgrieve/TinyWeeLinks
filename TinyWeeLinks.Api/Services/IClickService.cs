using System;
using TinyWeeLinks.Api.Data;

namespace TinyWeeLinks.Api.Services
{
    public interface IClickService
    {
        Link TrackClick(string shortcut);
    }
}
