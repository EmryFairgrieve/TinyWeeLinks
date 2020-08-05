using System.Collections.Generic;

namespace TinyWeeLinks.Api.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        bool Create(T entity);
        bool Update(T entity);
    }
}
