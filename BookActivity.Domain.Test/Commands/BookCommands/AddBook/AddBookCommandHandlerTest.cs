using BookActivity.Common.Test;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Domain.Test.Commands.BookCommands.AddBook
{
    public class AddBookCommandHandlerTest : BaseTest
    {
        [Test]
        public async Task TestTest2()
        {
            await BeginTransactionAsync(async (serviceProvider, dbContext) =>
            {
                var author = await DbDataCreator.CreateAuthorAsync(dbContext, DbConstants.AuthorFirstName, DbConstants.AuthorFirstName);
                var category = await DbDataCreator.CreateCategoryAsync(dbContext, DbConstants.CategoryTitle);

                var addBookCommandHandler = serviceProvider.GetRequiredService<IRequestHandler<AddBookCommand, ValidationResult>>();

                AddBookCommand addBookCommand = new(DbConstants.BookTitle, DbConstants.BookDescription, new[] { author.Id }, new[] { category.Id }, new byte[] { 1 });

                await addBookCommandHandler.Handle(addBookCommand, cancellationToken: default);

                var book = await dbContext.Books.FirstOrDefaultAsync(b => b.Title == DbConstants.BookTitle);

                Assert.IsNotNull(book);
                Assert.That(book.Description, Is.EqualTo(DbConstants.BookDescription));
                Assert.That(book.BookAuthors.First().AuthorId, Is.EqualTo(author.Id));
            });
        }
    }
}
