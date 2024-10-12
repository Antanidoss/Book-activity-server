using System;

namespace BookActivity.Domain.Commands.BookCommands.AddBook
{
    public sealed class AddBookCommand : BookCommand
    {
        public AddBookCommand() { }
        public AddBookCommand(string title, string description, Guid[] authorIds, Guid[] categoryIds, byte[] imageData)
        {
            Title = title;
            Description = description;
            AuthorIds = authorIds;
            CategoryIds = categoryIds;
            ImageData = imageData;
        }
    }
}