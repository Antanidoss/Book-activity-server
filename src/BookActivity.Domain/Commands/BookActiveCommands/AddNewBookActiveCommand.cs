using BookActivity.Domain.Commands.BookActiveCommands.Validations;
using System;

namespace BookActivity.Domain.Commands.BookActiveCommands
{
    public class AddNewBookActiveCommand : ActiveBookCommand
    {
        public AddNewBookActiveCommand(int totalNumberPages, int numberPagesRead, Guid bookId, Guid userId, bool isPublic)
        {
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
            BookId = bookId;
            UserId = userId;
            IsPublic = isPublic;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddNewActiveBookCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}