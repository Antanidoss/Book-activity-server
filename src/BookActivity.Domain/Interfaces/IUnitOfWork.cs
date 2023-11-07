using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
