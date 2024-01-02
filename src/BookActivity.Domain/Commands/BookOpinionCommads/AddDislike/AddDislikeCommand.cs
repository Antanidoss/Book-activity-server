using System;

namespace BookActivity.Domain.Commands.BookOpinionCommads.AddDislike
{
    public sealed class AddDislikeCommand : Command
    {
        public readonly Guid UserIdWhoDislike;
        public readonly Guid UserIdOpinion;
        public readonly Guid BookId;

        public AddDislikeCommand(Guid userIdWhoDislike, Guid bookId, Guid userIdOpinion)
        {
            UserIdWhoDislike = userIdWhoDislike;
            BookId = bookId;
            UserIdOpinion = userIdOpinion;
        }
    }
}
