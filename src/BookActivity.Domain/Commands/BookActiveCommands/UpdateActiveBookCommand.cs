﻿using System;

namespace BookActivity.Domain.Commands.BookActiveCommands
{
    public class UpdateActiveBookCommand : ActiveBookCommand
    {
        public UpdateActiveBookCommand(Guid bookActiveId, int numberPagesRead)
        {
            Id = bookActiveId;
            NumberPagesRead = numberPagesRead;
        }
    }
}