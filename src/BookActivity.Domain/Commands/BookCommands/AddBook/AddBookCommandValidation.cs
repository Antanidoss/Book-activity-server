using BookActivity.Domain.Commands.BookCommands.Validations;

namespace BookActivity.Domain.Commands.BookCommands.AddBook
{
    public sealed class AddBookCommandValidation : BookValidation<AddBookCommand>
    {
        public AddBookCommandValidation()
        {
            ValidationTitle();
            ValidationDescription();
            ValidationAuthors();
            ValidationCategories();
            ValidationImageData();
        }
    }
}
