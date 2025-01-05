using BookActivity.Domain.Cache;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.ActiveBookEvents;
using MediatR;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.ActiveBookStatisticEvents
{
    internal sealed class ActiveBookStatisticEventHandler :
        INotificationHandler<AddActiveBookAfterOperationEvent>,
        INotificationHandler<UpdateActiveBookEvent>,
        INotificationHandler<RemoveActiveBookEvent>
    {
        private readonly ActiveBookStatisticCache _cache;
        private readonly IMongoDatabase _mongoDb;

        public ActiveBookStatisticEventHandler(ActiveBookStatisticCache cache, IMongoDatabase mongoDb)
        {
            _cache = cache;
            _mongoDb = mongoDb;
        }

        public Task Handle(AddActiveBookAfterOperationEvent notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _cache.RemoveActiveBookStatistic(notification.UserId.Value);
            _cache.RemoveActiveBookStatisticByDay(notification.UserId.Value, DateTime.Now.Date);

            return Task.CompletedTask;
        }

        public Task Handle(UpdateActiveBookEvent notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _cache.RemoveActiveBookStatistic(notification.UserId.Value);
            _cache.RemoveActiveBookStatisticByDay(notification.UserId.Value, DateTime.Now.Date);

            return Task.CompletedTask;
        }

        public async Task Handle(RemoveActiveBookEvent notification, CancellationToken cancellationToken)
        {
            var updateActiveBookCollection = _mongoDb.GetCollection<UpdateActiveBookEvent>(EventMessageTypeConstants.UpdateActiveBook);
            var updateActiveBookEvents = updateActiveBookCollection
                .AsQueryable()
                .Where(r => r.AggregateId == notification.AggregateId)
                .ToArray();

            if (!updateActiveBookEvents.Any())
                return;

            _cache.RemoveActiveBookStatistic(notification.UserId.Value);

            foreach (var updateEvent in updateActiveBookEvents)
                _cache.RemoveActiveBookStatisticByDay(notification.UserId.Value, updateEvent.Timestamp.Date);

            await updateActiveBookCollection.DeleteManyAsync(r => r.AggregateId == notification.AggregateId);
        }
    }
}
