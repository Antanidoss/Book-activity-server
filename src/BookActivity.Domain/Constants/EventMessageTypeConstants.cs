using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.BookEvents;

namespace BookActivity.Domain.Constants
{
    public static class EventMessageTypeConstants
    {
        public const string AddActiveBook = nameof(AddActiveBookEvent);
        public const string RemoveActiveBook = nameof(RemoveActiveBookEvent);
        public const string UpdateActiveBook = nameof(UpdateActiveBookEvent);

        public const string AddBook = nameof(AddBookEvent);
        public const string RemoveBook = nameof(RemoveBookEvent);
        public const string UpdateBook = nameof(UpdateBookEvent);
    }
}
