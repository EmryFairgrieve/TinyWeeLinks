﻿using System;
using TinyWeeLinks.Api.Data;

namespace TinyWeeLinks.Api.Services
{
    public interface ILinkService
    {
        Link CreateLink(string url);
        Link FindLink(string shortcut, string secret);
    }
}
