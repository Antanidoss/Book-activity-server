using System;

namespace BookActivity.Domain.Commands.BookOpinionCommads.RemoveDislike
{
    public sealed class RemoveDislikeCommand : Command
    {
        public readonly Guid UserIdWhoDislike;
        public readonly Guid UserIdOpinion;
        public readonly Guid BookId;

        public RemoveDislikeCommand(Guid userIdWhoDislike, Guid bookId, Guid userIdOpinion)
        {
            UserIdWhoDislike = userIdWhoDislike;
            BookId = bookId;
            UserIdOpinion = userIdOpinion;
        }
    }
}
