using System;

namespace BookActivity.Domain.Commands.ActiveBookCommands
{
    public sealed class UpdateActiveBookCommand : ActiveBookCommand
    {
        public UpdateActiveBookCommand(Guid bookActiveId, int numberPagesRead)
        {
            Id = bookActiveId;
            NumberPagesRead = numberPagesRead;
        }
    }
}