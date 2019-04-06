using Microsoft.EntityFrameworkCore;
using System;

namespace Awaoa.Core.UnitOfWork
{
    public abstract class UnitOfWork<TDbContext> : IUnitOfWork, IDisposable
           where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;

        public UnitOfWork(TDbContext context)
        {
            dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual void Commit()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                dbContext.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}