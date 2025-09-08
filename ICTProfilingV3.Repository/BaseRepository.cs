using ICTProfilingV3.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using EntityManager.Context;
using System.Data.Entity;
using System.Linq;

namespace ICTProfilingV3.Repository
{
    public class BaseRepository<TKey , T> : IRepository<TKey, T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            if (entity == null)
                return;
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(TKey Id)
        {
            var entity = _dbSet.Find(Id);
            if (_context.Entry(entity).State == EntityState.Detached) _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void DeleteRange(Expression<Func<T, bool>> filter)
        {
            var entities = _dbSet.Where(filter);
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<T> Fetch(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public Task<T> GetByFilter(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.FirstOrDefaultAsync(filter);
        }

        public async Task<T> GetById(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
