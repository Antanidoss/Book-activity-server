using BookActivity.Domain.Queries;
using FluentValidation.Results;
using NetDevPack.Messaging;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Core.Events.Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
        Task<TResult> SendQuery<TResult>(Query<TResult> query);
    }
}
