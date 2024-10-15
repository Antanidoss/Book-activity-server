using BookActivity.Domain.Commands.ActiveBookCommands.Validations;

namespace BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook
{
    public sealed class AddNewActiveBookCommandValidation : ActiveBookValidation<AddActiveBookCommand>
    {
        public AddNewActiveBookCommandValidation()
        {
            ValidateNumberPagesRead();
            ValidateTotalNumberPages();
            ValidateUserId();
        }
    }
}