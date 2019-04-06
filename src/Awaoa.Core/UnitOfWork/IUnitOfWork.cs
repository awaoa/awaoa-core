using System;

namespace Awaoa.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}