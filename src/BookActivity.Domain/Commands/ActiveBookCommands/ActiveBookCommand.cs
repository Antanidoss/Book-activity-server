using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Commands.ActiveBookCommands
{
    public abstract class ActiveBookCommand : Command
    {
        public Guid Id { get; protected set; }
        public int TotalNumberPages { get; protected set; }
        public int NumberPagesRead { get; protected set; }
        public Guid BookId { get; protected set; }
        public Guid UserId { get; protected set; }
        public bool IsPublic { get; protected set; }
    }
}