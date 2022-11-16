using System;

namespace BookActivity.Domain.Commands.BookCommands.UpdateBook
{
    public sealed class UpdateBookCommand : BookCommand
    {
        public readonly Guid UserId;
    }
}
