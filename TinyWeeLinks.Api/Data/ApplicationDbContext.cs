using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TinyWeeLinks.Api.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        private DbSet<Link> Links { get; set; }
        private DbSet<Click> Clicks { get; set; }

        public void AddLink(Link entity)
        {
            Links.Add(entity);
        }

        public Link GetLink(string shortcut)
        {
            return Links.First(x => x.Shortcut == shortcut);
        }

        public void UpdateLink(Link entity)
        {
            Links.Update(entity);
        }
    }
}
