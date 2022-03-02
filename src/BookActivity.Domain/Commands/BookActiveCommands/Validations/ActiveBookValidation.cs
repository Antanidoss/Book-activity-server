using FluentValidation;
using System;

namespace BookActivity.Domain.Commands.BookActiveCommands.Validations
{
    public abstract class ActiveBookValidation<T> : AbstractValidator<T> where T : ActiveBookCommand
    {
        protected void ValidateNumberPagesRead()
        {
            RuleFor(a => a.NumberPagesRead)
                .NotEqual(0).WithMessage("The current page in the book cannot be equal to 0")
                .When(a => a.NumberPagesRead > a.TotalNumberPages).WithMessage("The current page in the book cannot be greater than the total number of pages in the book")
                .When(a => a.NumberPagesRead < 0).WithMessage("The current page in the book cannot be less than 0");
        }

        protected void ValidateTotalNumberPages()
        {
            RuleFor(a => a.TotalNumberPages)
                .NotEqual(0).WithMessage("The current page in the book cannot be equal to 0")
                .When(a => a.TotalNumberPages < 0).WithMessage("The total pages in the book cannot be less than 0");
        }

        protected void ValidateActiveBookId()
        {
            RuleFor(a => a.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateBookId()
        {
            RuleFor(a => a.BookId)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateUserId()
        {
            RuleFor(a => a.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}