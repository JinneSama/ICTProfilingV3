using ICTProfilingV3.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services.Base
{
    public class BaseDataService<T, TKey> : IBaseDataService<T, TKey>
        where T : class
    {
        public readonly IRepository<TKey, T> _baseRepo;
        public BaseDataService(IRepository<TKey, T> baseRepo)
        {
            _baseRepo = baseRepo;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _baseRepo.AddAsync(entity);
            await _baseRepo.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = _baseRepo.GetById(id);
            if (entity != null)
            {
                _baseRepo.Delete(id);
                await _baseRepo.SaveChangesAsync();
            }
        }

        public virtual async Task DeleteRangeAsync(Expression<Func<T, bool>> filter)
        {
            _baseRepo.DeleteRange(filter);
            await _baseRepo.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _baseRepo.GetAll();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            return await _baseRepo.GetByFilter(filter, includes);
        }

        public virtual async Task<T> GetByIdAsync(TKey id)
        {
            return await _baseRepo.GetById(id);
        }

        public async Task SaveChangesAsync()
        {
            await _baseRepo.SaveChangesAsync();
        }
    }
}
