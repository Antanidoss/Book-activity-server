using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class RemoveActiveBookEvent : Event
    {
        public RemoveActiveBookEvent(Guid activeBookId, Guid userId) : base(userId, activeBookId) {}
    }
}