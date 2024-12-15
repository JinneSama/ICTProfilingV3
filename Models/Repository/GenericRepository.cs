
using EntityManager.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace Models.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached) dbSet.Attach(entity);
            dbSet.Remove(entity);
        }

        public void DeleteByEx(Expression<Func<TEntity, bool>> expression)
        {
            TEntity entity = dbSet.FirstOrDefault(expression);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void DeleteRange(Expression<Func<TEntity, bool>> expression)
        {
            var entities = FindAllAsync(expression);
            dbSet.RemoveRange(entities);
        }

        public IQueryable<TEntity> FindAllAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.Where(expression);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(expression).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
    }
}
