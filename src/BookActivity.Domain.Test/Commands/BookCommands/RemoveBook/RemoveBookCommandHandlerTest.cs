using BookActivity.Common.Test;
using BookActivity.Domain.Commands.BookCommands.RemoveBook;
using BookActivity.Domain.Commands.BookCommands.Validations;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Domain.Test.Commands.BookCommands.RemoveBook
{
    public class RemoveBookCommandHandlerTest : BaseTest
    {
        [Test]
        public async Task RemoveBook_Ok_Async()
        {
            await BeginTransactionAsync(async (serviceProvider, dbContext) =>
            {
                var book = await DbDataCreator.CreateBookAsync(dbContext);

                dbContext.ClearChangeTracker();

                RemoveBookCommand removeBookCommand = new(book.Id, _currentUser.Id);
                var removeBookCommandHandler = serviceProvider.GetRequiredService<IRequestHandler<RemoveBookCommand, ValidationResult>>();

                await removeBookCommandHandler.Handle(removeBookCommand, cancellationToken: default);

                Assert.IsNull(await dbContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id));
            });
        }

        [Test]
        public async Task RemoveBook_CheckValidation_Failure_Async()
        {
            await BeginTransactionAsync(async (serviceProvider, dbContext) =>
            {
                RemoveBookCommand removeBookCommand = new(bookId: Guid.Empty, userId: Guid.Empty);
                var removeBookCommandHandler = serviceProvider.GetRequiredService<IRequestHandler<RemoveBookCommand, ValidationResult>>();

                var validationResult = await removeBookCommandHandler.Handle(removeBookCommand, cancellationToken: default);

                var bookIdEmptyError = validationResult.Errors.Find(v => v.ErrorMessage == ValidationMessageConstants.BookIdСannotBeEmpty);
                Assert.IsNotNull(bookIdEmptyError);

                var userIdEmptyError = validationResult.Errors.Find(v => v.ErrorMessage == ValidationMessageConstants.UserIdСannotBeEmpty);
                Assert.IsNotNull(userIdEmptyError);
            });
        }
    }
}
