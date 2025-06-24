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
        public Task AddAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> Fetch(Expression<Func<T, bool>> filter)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<T> GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
