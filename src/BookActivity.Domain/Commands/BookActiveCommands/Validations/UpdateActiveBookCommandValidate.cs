namespace BookActivity.Domain.Commands.BookActiveCommands.Validations
{
    public class UpdateActiveBookCommandValidate : ActiveBookValidation<UpdateActiveBookCommand>
    {
        public UpdateActiveBookCommandValidate()
        {
            ValidateActiveBookId();
            ValidateNumberPagesRead();
        }
    }
}