﻿namespace BookActivity.Domain.Commands.ActiveBookCommands.Validations
{
    public sealed class UpdateActiveBookCommandValidate : ActiveBookValidation<UpdateActiveBookCommand>
    {
        public UpdateActiveBookCommandValidate()
        {
            ValidateActiveBookId();
            ValidateNumberPagesRead();
        }
    }
}