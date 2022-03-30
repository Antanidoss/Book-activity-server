using BookActivity.Domain.Core.Events;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        private readonly IEventStore _eventStore;

        public InMemoryBus(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStore?.Save(@event);

            await _mediator.Publish(@event);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}