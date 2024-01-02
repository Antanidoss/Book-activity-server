using System;

namespace BookActivity.Domain.Commands.BookOpinionCommads.RemoveLike
{
    public sealed class RemoveLikeCommand : Command
    {
        public readonly Guid UserIdWhoLike;
        public readonly Guid UserIdOpinion;
        public readonly Guid BookId;

        public RemoveLikeCommand(Guid userIdWhoLike, Guid bookId, Guid userIdOpinion)
        {
            UserIdWhoLike = userIdWhoLike;
            BookId = bookId;
            UserIdOpinion = userIdOpinion;
        }
    }
}
