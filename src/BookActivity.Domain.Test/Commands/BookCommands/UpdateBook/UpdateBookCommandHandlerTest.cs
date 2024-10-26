using BookActivity.Common.Test;
using BookActivity.Domain.Commands.BookCommands.UpdateBook;
using BookActivity.Domain.Commands.BookCommands.Validations;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Domain.Test.Commands.BookCommands.UpdateBook
{
    public class UpdateBookCommandHandlerTest : BaseTest
    {
        const string _newTitle = "New title";
        const string _newDescription = "New description";

        [Test]
        public async Task UpdateBook_Ok_Async()
        {
            await BeginTransactionAsync(async (serviceProvider, dbContext) =>
            {
                var book = await DbDataCreator.CreateBookAsync(dbContext);

                var updateBookCommandHandler = serviceProvider.GetRequiredService<IRequestHandler<UpdateBookCommand, ValidationResult>>();

                UpdateBookCommand updateBookCommand = new()
                {
                    BookId = book.Id,
                    UserId = _currentUser.Id,
                    Title = _newTitle,
                    Description = _newDescription
                };

                await updateBookCommandHandler.Handle(updateBookCommand, cancellationToken: default);

                book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id);

                Assert.NotNull(book);
                Assert.That(book.Title, Is.EqualTo(_newTitle));
                Assert.That(book.Description, Is.EqualTo(_newDescription));
            });
        }

        [Test]
        public async Task UpdateBook_CheckValidation_Failure_Async()
        {
            await BeginTransactionAsync(async (serviceProvider, dbContext) =>
            {
                var book = await DbDataCreator.CreateBookAsync(dbContext);

                UpdateBookCommand updateBookCommand = new()
                {
                    Title = _newTitle,
                    Description = _newDescription
                };

                var updateBookCommandHandler = serviceProvider.GetRequiredService<IRequestHandler<UpdateBookCommand, ValidationResult>>();

                var validationResult = await updateBookCommandHandler.Handle(updateBookCommand, cancellationToken: default);

                book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id);

                Assert.NotNull(book);
                Assert.That(book.Title, Is.EqualTo(DbConstants.BookTitle));
                Assert.That(book.Description, Is.EqualTo(DbConstants.BookDescription));

                var userIdEmptyError = validationResult.Errors.Find(v => v.ErrorMessage == ValidationMessageConstants.UserIdСannotBeEmpty);
                Assert.IsNotNull(userIdEmptyError);

                var bookIdEmptyError = validationResult.Errors.Find(v => v.ErrorMessage == ValidationMessageConstants.BookIdСannotBeEmpty);
                Assert.IsNotNull(bookIdEmptyError);
            });
        }
    }
}
