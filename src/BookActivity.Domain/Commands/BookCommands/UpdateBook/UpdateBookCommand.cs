namespace BookActivity.Domain.Commands.BookCommands.UpdateBook
{
    public sealed class UpdateBookCommand : BookCommand
    {
        public UpdateBookCommand() { }
        public UpdateBookCommand(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
