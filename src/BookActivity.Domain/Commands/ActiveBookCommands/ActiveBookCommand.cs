using System;

namespace BookActivity.Domain.Commands.ActiveBookCommands
{
    public abstract class ActiveBookCommand : Command
    {
        public Guid Id { get; set; }
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
    }
}