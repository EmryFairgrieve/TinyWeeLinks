using System;
using System.Collections.Generic;

namespace TinyWeeLinks.Api.Data
{
    public interface IApplicationDbContext
    {
        int SaveChanges();
        void AddLink(Link entity);
        Link GetLink(string shortcut);
        void AddClick(Click entity);
        ICollection<Link> GetLinks();
    }
}
