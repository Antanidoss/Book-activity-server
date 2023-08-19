using BookActivity.Domain.Core.Events;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Queries;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure
{
    internal sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        private readonly IEventStore _eventStore;

        public InMemoryBus(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }

        public async Task PublishEventsAsync<T>(IEnumerable<T> events, CancellationToken cancellationToken = default) where T : Domain.Core.Events.Event
        {
            foreach (var e in events)
            {
                if (!e.MessageType.Equals("DomainNotification"))
                    _eventStore?.Save(e);

                await _mediator.Publish(e, cancellationToken);
            }
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