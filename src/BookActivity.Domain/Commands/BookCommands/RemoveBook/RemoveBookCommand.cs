﻿using System;

namespace BookActivity.Domain.Commands.BookCommands.RemoveBook
{
    public sealed class RemoveBookCommand : BookCommand
    {
        public readonly Guid UserId;
        public RemoveBookCommand(Guid bookId, Guid userId)
        {
            BookId = bookId;
            UserId = userId;
        }
    }
}