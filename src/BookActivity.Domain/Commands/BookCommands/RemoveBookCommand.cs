using System;

namespace BookActivity.Domain.Commands.BookCommands
{
    public sealed class RemoveBookCommand : BookCommand
    {
        public RemoveBookCommand(Guid bookId)
        {
            BookId = bookId;
        }
    }
}