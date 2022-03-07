using BookActivity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public class ActiveBookEventHandler :
        INotificationHandler<AddActiveBookEvent>,
        INotificationHandler<UpdateActiveBookEvent>,
        INotificationHandler<RemoveActiveBookEvent>
    {
        public async Task Handle(AddActiveBookEvent notification, CancellationToken cancellationToken)
        {
            
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
