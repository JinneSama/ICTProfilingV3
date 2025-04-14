using EntityManager.Context;
using EntityManager.Interfaces;
using EntityManager.Utility;
using Models.Entities;
using Models.Managers.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
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

        public void Delete(TEntity entity)
        {
            TEntity oldValues = entity;
            if (dbContext.Entry(entity).State == EntityState.Detached) dbSet.Attach(entity);
            dbSet.Remove(entity);
            LogChangeAsync(typeof(TEntity).Name, "Delete", oldValues, null);
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
            LogChangeAsync(typeof(TEntity).Name, "Insert", null, entity);
        }

        public void LogChangeAsync(string tableName, string actionType, object oldValues, object newValues)
        {
            var cleanedOldValues = CleanObject(oldValues);
            var cleanedNewValues = CleanObject(newValues);

            var logEntry = new LogEntry
            {
                Date = DateTime.UtcNow,
                TableName = tableName,
                ActionType = actionType,
                OldValues = cleanedOldValues != null
                    ? JsonConvert.SerializeObject(cleanedOldValues, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                    : null,
                NewValues = cleanedNewValues != null
                    ? JsonConvert.SerializeObject(cleanedNewValues, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                    : null,
                CreatedById = UserStore.UserId,
                MacAddress = _machineCredentials.GetMacAddress(),
                PCName = _machineCredentials.GetPCName()
            };

            dbContext.Set<LogEntry>().Add(logEntry);
            dbContext.SaveChanges();
        }

        private object CleanObject(object obj)
        {
            if (obj == null) return null;

            var type = obj.GetType();
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dict = new Dictionary<string, object>();

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);
                if (value == null) continue;

                if (value is string strVal && string.IsNullOrWhiteSpace(strVal)) continue;

                dict[prop.Name] = value;
            }

            return dict;
        }

        public void Update(TEntity entity)
        {
            var existingEntity = dbSet.Find(dbContext.Entry(entity).Property("Id").CurrentValue);
            var oldValues = dbContext.Entry(existingEntity).CurrentValues.Clone();

            dbContext.Entry(entity).State = EntityState.Modified;
            LogChangeAsync(typeof(TEntity).Name, "Update", oldValues.ToObject(), entity);
        }

        public void TruncateEntity()
        {
            var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            var entitySet = objectContext
                .MetadataWorkspace
                .GetItems<EntityContainer>(DataSpace.SSpace)
                .Single()
                .EntitySets
                .FirstOrDefault(s => s.ElementType.Name == typeof(TEntity).Name);

            var tableName = entitySet?.Table ?? entitySet?.Name;

            dbContext.Database.ExecuteSqlCommand($@"Truncate Table [{tableName}]");
        }
    }
}
