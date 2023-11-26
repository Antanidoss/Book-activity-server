using System;

namespace BookActivity.Domain.Commands.BookOpinionCommads.AddBookOpinion
{
    public sealed class AddBookOpinionCommand : Command
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public float Grade { get; set; }
        public string Descriptions {  get; set; }
    }
}
