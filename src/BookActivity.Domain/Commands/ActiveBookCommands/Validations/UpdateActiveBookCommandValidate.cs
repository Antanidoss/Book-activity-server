namespace BookActivity.Domain.Commands.ActiveBookCommands.Validations
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