using System;

namespace BookActivity.Domain.Commands.BookOpinionCommads.AddLike
{
    public sealed class AddLikeCommand : Command
    {
        public readonly Guid UserIdWhoLike;
        public readonly Guid UserIdOpinion;
        public readonly Guid BookId;

        public AddLikeCommand(Guid userWhoLikeId, Guid bookId, Guid userIdOpinion)
        {
            UserIdWhoLike = userWhoLikeId;
            BookId = bookId;
            UserIdOpinion = userIdOpinion;
        }
    }
}
