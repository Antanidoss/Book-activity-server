using BookActivity.Domain.Commands.BookCommands.Validations;
using FluentValidation;
using System;

namespace BookActivity.Domain.Commands.BookCommands.UpdateBook
{
    public sealed class UpdateBookCommandValidation : BookValidation<UpdateBookCommand>
    {
        public UpdateBookCommandValidation()
        {
            ValidationBookId();
            ValidationUserId();
        }

        private void ValidationUserId()
        {
            RuleFor(a => a.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage(ValidationMessageConstants.UserIdСannotBeEmpty);
        }
    }
}
