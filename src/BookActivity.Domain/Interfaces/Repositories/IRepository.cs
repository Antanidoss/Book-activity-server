using BookActivity.Domain.Core;
using NetDevPack.Data;
using System;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
