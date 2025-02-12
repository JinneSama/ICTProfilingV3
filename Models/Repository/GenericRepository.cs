using EntityManager.Context;
using EntityManager.Interfaces;
using EntityManager.Utility;
using Models.Entities;
using Models.Managers.User;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;
        private readonly IMachineCredentials _machineCredentials;

        public GenericRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
            dbSet = dbContext.Set<TEntity>();
            _machineCredentials = new MachineCredentials();
        }

        public async void Delete(TEntity entity)
        {
            TEntity oldValues = entity;
            if (dbContext.Entry(entity).State == EntityState.Detached) dbSet.Attach(entity);
            dbSet.Remove(entity);
            await LogChangeAsync(typeof(TEntity).Name, "Delete", oldValues, null);
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

        public async void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            await LogChangeAsync(typeof(TEntity).Name, "Insert", null, entity);
        }

        public async Task LogChangeAsync(string tableName, string actionType, object oldValues, object newValues)
        {
            var logEntry = new LogEntry
            {
                Date = DateTime.UtcNow,
                TableName = tableName,
                ActionType = actionType,
                OldValues = oldValues != null ? JsonConvert.SerializeObject(oldValues, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }) : null,
                NewValues = newValues != null ? JsonConvert.SerializeObject(newValues, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }) : null,
                CreatedById = UserStore.UserId,
                MacAddress = _machineCredentials.GetMacAddress(),
                PCName = _machineCredentials.GetPCName()
            };

            dbContext.Set<LogEntry>().Add(logEntry);
            await dbContext.SaveChangesAsync();
        }

        public async void Update(TEntity entity)
        {
            var existingEntity = dbSet.Find(dbContext.Entry(entity).Property("Id").CurrentValue);
            var oldValues = dbContext.Entry(existingEntity).CurrentValues.Clone();

            dbContext.Entry(entity).State = EntityState.Modified;
            await LogChangeAsync(typeof(TEntity).Name, "Update", oldValues.ToObject(), entity);
        }
    }
}
