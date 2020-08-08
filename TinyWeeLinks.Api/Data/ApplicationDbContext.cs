using System.Collections.Generic;
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

        public void AddClick(Click entity)
        {
            Clicks.Add(entity);
        }

        public void AddLink(Link entity)
        {
            Links.Add(entity);
        }

        public Link GetLink(string shortcut)
        {
            return Links
                .Include(l => l.Clicks)
                .FirstOrDefault(x => x.Shortcut == shortcut);
        }

        public ICollection<Link> GetLinks()
        {
            return Links.ToList();
        }

        public void UpdateLink(Link entity)
        {
            var existingLink = Links
                .Where(l => l.Id == entity.Id)
                .Include(l => l.Clicks)
                .SingleOrDefault();

            if (existingLink != null)
            {
                foreach (var clickModel in entity.Clicks)
                {
                    var existingChild = existingLink.Clicks
                        .Where(c => c.Id == clickModel.Id)
                        .SingleOrDefault();

                    if (existingChild == null)
                    {
                        // Insert child
                        var newClick = new Click
                        {
                            Id = clickModel.Id,
                            DateTimeClicked = clickModel.DateTimeClicked
                        };
                        existingLink.Clicks.Add(newClick);
                    }
                }
            }
        }
    }
}
