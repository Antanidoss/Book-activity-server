using BookActivity.Domain.Cache;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.ActiveBookEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.ActiveBookStatisticEvents
{
    internal sealed class ActiveBookStatisticEventHandler :
        INotificationHandler<AddActiveBookAfterOperationEvent>,
        INotificationHandler<UpdateActiveBookEvent>
    {
        private readonly ActiveBookStatisticCache _cache;

        public ActiveBookStatisticEventHandler(ActiveBookStatisticCache cache)
        {
            _cache = cache;
        }

        public Task Handle(AddActiveBookAfterOperationEvent notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _cache.Remove(notification.UserId.Value);

            return Task.CompletedTask;
        }

        public Task Handle(UpdateActiveBookEvent notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _cache.Remove(notification.UserId.Value);

            return Task.CompletedTask;
        }
    }
}
