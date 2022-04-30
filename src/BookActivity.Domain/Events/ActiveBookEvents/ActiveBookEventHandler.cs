using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class ActiveBookEventHandler :
        INotificationHandler<AddActiveBookEvent>,
        INotificationHandler<UpdateActiveBookEvent>,
        INotificationHandler<RemoveActiveBookEvent>
    {
        public Task Handle(AddActiveBookEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(UpdateActiveBookEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(RemoveActiveBookEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
