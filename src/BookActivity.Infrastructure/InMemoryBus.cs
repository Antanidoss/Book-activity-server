using BookActivity.Domain.Commands;
using BookActivity.Domain.Core;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Queries;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure
{
    internal sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEventsAsync<T>(IEnumerable<T> events, CancellationToken cancellationToken = default) where T : Event
        {
            await Parallel.ForEachAsync(events, cancellationToken, async (e, c) => await _mediator.Publish(e, c));
        }

        public async Task<ValidationResult> SendCommandAsync<T>(T command, CancellationToken cancellationToken = default) where T : Command
        {
            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<TResult> SendQueryAsync<TResult>(Query<TResult> query, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(query, cancellationToken);
        }
    }
}