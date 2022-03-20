using BookActivity.Domain.Commands.ActiveBookCommands.Validations;
using System;

namespace BookActivity.Domain.Commands.ActiveBookCommands
{
    public class AddActiveBookCommand : ActiveBookCommand
    {
        public AddActiveBookCommand(int totalNumberPages, int numberPagesRead, Guid bookId, Guid userId, bool isPublic)
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