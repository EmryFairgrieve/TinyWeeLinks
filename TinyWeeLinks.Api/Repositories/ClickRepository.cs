using System;
using TinyWeeLinks.Api.Data;

namespace TinyWeeLinks.Api.Repositories
{
    public class ClickRepository : IClickRepository
    {
        private readonly IApplicationDbContext _db;

        public ClickRepository(IApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Click entity)
        {
            _db.AddClick(entity);
            return Save();
        }

        private bool Save()
        {
            return _db.SaveChanges() > 0;
        }
    }
}
