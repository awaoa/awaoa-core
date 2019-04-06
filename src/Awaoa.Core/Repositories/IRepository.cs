using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Awaoa.Core.Entities;

namespace Awaoa.Core.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, int>
               where TEntity : class, IEntity
    {
    }

    public interface IRepository<TEntity, TPrimaryKey>
               where TEntity : class, IEntity<TPrimaryKey>
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                 string includeProperties = "");

        TEntity GetByID(TPrimaryKey id);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entityToUpdate);

        void Delete(TPrimaryKey id);

        void Delete(TEntity entityToDelete);
    }
}