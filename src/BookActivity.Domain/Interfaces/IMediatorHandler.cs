using BookActivity.Domain.Commands;
using BookActivity.Domain.Core;
using BookActivity.Domain.Queries;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEventsAsync<T>(IEnumerable<T> events, CancellationToken cancellationToken = default) where T : Event;
        Task<ValidationResult> SendCommandAsync<T>(T command, CancellationToken cancellationToken = default) where T : Command;
        Task<TResult> SendQueryAsync<TResult>(Query<TResult> query, CancellationToken cancellationToken = default);
    }
}
