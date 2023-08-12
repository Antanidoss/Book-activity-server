using BookActivity.Domain.Queries;
using FluentValidation.Results;
using NetDevPack.Messaging;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEventAsync<T>(T @event) where T : Core.Events.Event;
        Task<ValidationResult> SendCommandAsync<T>(T command) where T : Command;
        Task<TResult> SendQueryAsync<TResult>(Query<TResult> query);
    }
}
