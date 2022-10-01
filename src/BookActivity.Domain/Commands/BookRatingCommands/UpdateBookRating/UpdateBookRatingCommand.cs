using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Commands.BookRatingCommands.UpdateBookRating
{
    public sealed class UpdateBookRatingCommand : BookRatingCommand
    {
        public BookOpinion BookOpinion { get; set; }
    }
}
