using System;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IITransactionRepository
    {
        bool InTransaction(Action action);
    }
}
