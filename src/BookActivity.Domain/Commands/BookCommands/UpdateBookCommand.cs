using BookActivity.Domain.Models;
using System.Collections.Generic;

namespace BookActivity.Domain.Commands.BookCommands
{
    public class UpdateBookCommand : BookCommand
    {
        public UpdateBookCommand(string title, string description, IEnumerable<BookOpinion> bookOpinions)
        {
            Title = title;
            Description = description;
            BookOpinions = bookOpinions;
        }
    }
}
