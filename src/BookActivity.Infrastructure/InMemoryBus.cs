using BookActivity.Domain.Core.Events;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Queries;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
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

        public async Task PublishEvent<T>(T @event) where T : Domain.Core.Events.Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStore?.Save(@event);

            await _mediator.Publish(@event);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task<TResult> SendQuery<TResult>(Query<TResult> query)
        {
            return await _mediator.Send(query);
        }
    }
}