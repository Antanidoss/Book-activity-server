using BookActivity.Common.Test;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using BookActivity.Domain.Commands.BookCommands.Validations;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Domain.Test.Commands.BookCommands.AddBook
{
    public class AddBookCommandHandlerTest : BaseTest
    {
        [Test]
        public async Task AddBook_Ok_Async()
        {
            await BeginTransactionAsync(async (serviceProvider, dbContext) =>
            {
                var author = await DbDataCreator.CreateAuthorAsync(dbContext, DbConstants.AuthorFirstName, DbConstants.AuthorFirstName);
                var category = await DbDataCreator.CreateCategoryAsync(dbContext, DbConstants.CategoryTitle);

                var addBookCommandHandler = serviceProvider.GetRequiredService<IRequestHandler<AddBookCommand, ValidationResult>>();

                AddBookCommand addBookCommand = new(DbConstants.BookTitle, DbConstants.BookDescription, new[] { author.Id }, new[] { category.Id }, DbConstants.ImageData);

                await addBookCommandHandler.Handle(addBookCommand, cancellationToken: default);

                var book = await dbContext.Books.FirstOrDefaultAsync(b => b.Title == DbConstants.BookTitle);

                Assert.IsNotNull(book);
                Assert.That(book.Description, Is.EqualTo(DbConstants.BookDescription));
                Assert.That(book.BookAuthors.First().AuthorId, Is.EqualTo(author.Id));
            });
        }

        [Test]
        public async Task AddBook_CheckValidation_Failure_Async()
        {
            await BeginTransactionAsync(async (serviceProvider, dbContext) =>
            {
                var addBookCommandHandler = serviceProvider.GetRequiredService<IRequestHandler<AddBookCommand, ValidationResult>>();

                AddBookCommand addBookCommand = new();

                var validationResult = await addBookCommandHandler.Handle(addBookCommand, cancellationToken: default);

                var book = await dbContext.Books.FirstOrDefaultAsync(b => b.Title == DbConstants.BookTitle);

                Assert.IsNull(book);

                var titileEmptyError = validationResult.Errors.Find(v => v.ErrorMessage == ValidationMessageConstants.TitleСannotBeEmpty);
                Assert.IsNotNull(titileEmptyError);

                var descriptionEmptyError = validationResult.Errors.Find(v => v.ErrorMessage == ValidationMessageConstants.DescriptionСannotBeEmpty);
                Assert.IsNotNull(descriptionEmptyError);

                var authorsEmptyError = validationResult.Errors.Find(v => v.ErrorMessage == ValidationMessageConstants.AuthorsСannotBeEmpty);
                Assert.IsNotNull(authorsEmptyError);

                var categoriesEmptyError = validationResult.Errors.Find(v => v.ErrorMessage == ValidationMessageConstants.CategoriesСannotBeEmpty);
                Assert.IsNotNull(categoriesEmptyError);

                var imageEmptyError = validationResult.Errors.Find(v => v.ErrorMessage == ValidationMessageConstants.ImageСannotBeEmpty);
                Assert.IsNotNull(imageEmptyError);
            });
        }
    }
}
