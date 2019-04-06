using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Awaoa.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Awaoa.Core.Repositories
{
    public class GenericRepository<TEntity> : GenericRepository<TEntity, int>, IRepository<TEntity>
           where TEntity : class, IEntity
    {
        public GenericRepository(DbContext context) : base(context)
        { }
    }

    public class GenericRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
           where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly DbContext dbContext;

        public DbSet<TEntity> Table => dbContext.Set<TEntity>();

        public GenericRepository(DbContext context)
        {
            dbContext = context;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = Table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(TPrimaryKey id)
        {
            return Table.Find(id);
        }

        public virtual TEntity Create(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void Delete(TPrimaryKey id)
        {
            TEntity entityToDelete = Table.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }
    }
}