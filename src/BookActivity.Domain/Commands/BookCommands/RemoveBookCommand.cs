using System;

namespace BookActivity.Domain.Commands.BookCommands
{
    public class RemoveBookCommand : BookCommand
    {
        public RemoveBookCommand(Guid bookId)
        {
            BookId = bookId;
        }
    }
}