using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public class ActiveBookEventHandler :
        INotificationHandler<AddActiveBookEvent>,
        INotificationHandler<UpdateActiveBookEvent>,
        INotificationHandler<RemoveActiveBookEvent>
    {
        public Task Handle(AddActiveBookEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(UpdateActiveBookEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(RemoveActiveBookEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
