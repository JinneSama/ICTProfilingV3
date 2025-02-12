using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class 
    {
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression,params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> FindAllAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(Expression<Func<TEntity, bool>> expression);
        void DeleteByEx(Expression<Func<TEntity, bool>> expression);    
    }
}
