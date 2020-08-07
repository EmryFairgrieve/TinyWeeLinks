using TinyWeeLinks.Api.Data;

namespace TinyWeeLinks.Api.Repositories
{
    public interface ILinkRepository : IRepositoryBase<Link>
    {
        Link FindByShortcut(string shortcut);
        bool Update(Link link);
    }
}
