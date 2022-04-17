using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Commands.BookCommands
{
    public class AddBookCommand : BookCommand
    {
        public AddBookCommand() { }
        public AddBookCommand(string title, string description, IEnumerable<Guid> authorIds)
        {
            Title = title;
            Description = description;
            AuthorIds = authorIds;
        }
    }
}