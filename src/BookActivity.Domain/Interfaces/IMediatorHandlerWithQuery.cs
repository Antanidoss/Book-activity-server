using BookActivity.Domain.Queries;
using NetDevPack.Mediator;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces
{
    public interface IMediatorHandlerWithQuery : IMediatorHandler
    {
        Task<TResult> SendQuery<TResult>(Query<TResult> query);
    }
}
