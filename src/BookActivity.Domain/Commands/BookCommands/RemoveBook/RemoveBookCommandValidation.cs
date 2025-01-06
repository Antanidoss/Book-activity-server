using BookActivity.Domain.Commands.BookCommands.Validations;
using FluentValidation;

namespace BookActivity.Domain.Commands.BookCommands.RemoveBook
{
    public sealed class RemoveBookCommandValidation : BookValidation<RemoveBookCommand>
    {
        public RemoveBookCommandValidation()
        {
            ValidationBookId();
            ValidationUserId();
        }

        private void ValidationUserId()
        {
            RuleFor(a => a.UserId)
                .NotEmpty()
                .WithMessage(ValidationMessageConstants.UserIdСannotBeEmpty);
        }
    }
}
