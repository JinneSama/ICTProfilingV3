using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IRepository<TKey, T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Fetch(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task<T> GetById(TKey id);
        Task SaveChangesAsync();
        void Delete(TKey Id);
        void DeleteRange(Expression<Func<T, bool>> filter);
    }
}
