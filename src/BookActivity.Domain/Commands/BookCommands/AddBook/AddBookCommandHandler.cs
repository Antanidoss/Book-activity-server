using BookActivity.Domain.Events.BookEvents;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookCommands.AddBook
{
    internal sealed class AddBookCommandHandler : CommandHandler,
        IRequestHandler<AddBookCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public AddBookCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            var bookAuthors = request.AuthorIds.Select(a => new BookAuthor(a));
            var bookCategories = request.CategoryIds.Select(c => new BookCategory(c)).ToArray();
            Book newBook = new(request.Title, request.Description, request.ImageData, bookAuthors, bookCategories);

            newBook.AddDomainEvent(new AddBookEvent(newBook.Id, newBook.Title, newBook.Description, request.AuthorIds));
            await _efContext.Books.AddAsync(newBook);

            return await Commit(_efContext);
        }
    }
}
