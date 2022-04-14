using System.Collections.Generic;

namespace BookActivity.Domain.Commands.BookCommands
{
    public class AddBookCommand : BookCommand
    {
        public AddBookCommand(string title, string description, IEnumerable<int> authorIds)
        {
            Title = title;
            Description = description;
            AuthorIds = authorIds;
        }
    }
}