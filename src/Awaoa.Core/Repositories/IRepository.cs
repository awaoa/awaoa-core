using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
        Task<IEnumerable<TEntity>> GetAync(Expression<Func<TEntity, bool>> filter = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                 params string[] includeProperties);

        Task<TEntity> GetByIDAync(TPrimaryKey id);

        Task<TEntity> CreateAync(TEntity entity);

        Task<TEntity> UpdateAync(TEntity entityToUpdate);

        Task DeleteAync(TPrimaryKey id);

        Task DeleteAync(TEntity entityToDelete);

        Task<int> CountAync();
    }
}