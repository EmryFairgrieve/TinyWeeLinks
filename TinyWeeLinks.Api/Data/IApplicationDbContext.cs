using System;
namespace TinyWeeLinks.Api.Data
{
    public interface IApplicationDbContext
    {
        int SaveChanges();
        void AddLink(Link entity);
        Link GetLink(string shortcut);
        void AddClick(Click entity);
        void UpdateLink(Link entity);
    }
}
