using System.Collections.Generic;
using TinyWeeLinks.Api.Data;

namespace TinyWeeLinks.Api.Repositories
{
    public interface ILinkRepository : IRepositoryBase<Link>
    {
        Link FindByShortcut(string shortcut);
        ICollection<Link> GetLinks();
    }
}
