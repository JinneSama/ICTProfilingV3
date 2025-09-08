using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IBaseDataService<T , TKey> where T : class
    {
        Task SaveChangesAsync();
        Task<T> GetByIdAsync(TKey id);
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll();
        Task DeleteAsync(TKey id);
        Task DeleteRangeAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
    }
}
