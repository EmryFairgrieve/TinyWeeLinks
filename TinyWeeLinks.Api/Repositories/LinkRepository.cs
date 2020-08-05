using System;
using System.Collections.Generic;
using System.Linq;
using TinyWeeLinks.Api.Data;

namespace TinyWeeLinks.Api.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly IApplicationDbContext _db;

        public LinkRepository(IApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Link entity)
        {
            _db.AddLink(entity);
            return Save();
        }

        public Link FindByShortcut(string shortcut)
        {
            return _db.GetLink(shortcut);
        }

        public bool Update(Link entity)
        {
            _db.UpdateLink(entity);
            return Save();
        }

        private bool Save()
        {
            return _db.SaveChanges() > 0;
        }
    }
}
