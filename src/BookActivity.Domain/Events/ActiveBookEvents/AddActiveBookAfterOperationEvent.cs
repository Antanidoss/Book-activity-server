using BookActivity.Domain.Core;
using BookActivity.Domain.Events.ActiveBookEvent;

namespace BookActivity.Domain.Events.ActiveBookEvents
{
    public sealed class AddActiveBookAfterOperationEvent : AddActiveBookEvent
    {
        public override WhenCallHandler WhenCallHandler { get; } = WhenCallHandler.AfterSave;

        public AddActiveBookAfterOperationEvent(AddActiveBookEvent @event) : base(@event.AggregateId, @event.TotalNumberPages, @event.NumberPagesRead, @event.BookId, @event.UserId.Value) { }
    }
}
