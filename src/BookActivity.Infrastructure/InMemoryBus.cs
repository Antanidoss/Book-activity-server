using BookActivity.Domain.Core.Events;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Queries;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure
{
    internal sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        private readonly IEventStore _eventStore;

        private readonly ILogger<InMemoryBus> _logger;

        public InMemoryBus(IMediator mediator, IEventStore eventStore, ILogger<InMemoryBus> logger)
        {
            _mediator = mediator;
            _eventStore = eventStore;
            _logger = logger;
        }

        public async Task PublishEventsAsync<T>(IEnumerable<T> events, CancellationToken cancellationToken = default) where T : Domain.Core.Events.Event
        {
            Parallel.ForEach(events, async e =>
            {
                if (!e.MessageType.Equals("DomainNotification"))
                    _eventStore?.Save(e);

                try
                {
                    await _mediator.Publish(e, cancellationToken);
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogError(ex, null);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, null);
                }
            });
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