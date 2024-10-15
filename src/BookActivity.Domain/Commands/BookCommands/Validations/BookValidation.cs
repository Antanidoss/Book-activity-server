using FluentValidation;
using System;

namespace BookActivity.Domain.Commands.BookCommands.Validations
{
    public abstract class BookValidation<T> : AbstractValidator<T> where T : BookCommand
    {
        protected void ValidationTitle()
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessageConstants.TitleСannotBeEmpty);
        }

        protected void ValidationDescription()
        {
            RuleFor(a => a.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessageConstants.DescriptionСannotBeEmpty);
        }

        protected void ValidationAuthors()
        {
            RuleFor(a => a.AuthorIds)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessageConstants.AuthorsСannotBeEmpty);
        }

        protected void ValidationCategories()
        {
            RuleFor(a => a.CategoryIds)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessageConstants.CategoriesСannotBeEmpty);
        }

        protected void ValidationImageData()
        {
            RuleFor(a => a.ImageData)
                .NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessageConstants.ImageСannotBeEmpty);
        }

        protected void ValidationBookId()
        {
            RuleFor(a => a.BookId)
                .NotEqual(Guid.Empty)
                .WithMessage(ValidationMessageConstants.BookIdСannotBeEmpty);
        }
    }
}
