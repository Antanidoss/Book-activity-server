using BookActivity.Domain.Events.BookEvents;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Specifications.BookSpecs;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookCommands.UpdateBook
{
    internal sealed class UpdateBookCommandHandler : CommandHandler,
        IRequestHandler<UpdateBookCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public UpdateBookCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookByIdSpec bookByIdSpec = new(request.BookId);
            var book = await _efContext.Books.FirstAsync(bookByIdSpec);

            book.Title = request.Title;
            book.Description = request.Description;
            book.ImageData = request.ImageData;

            book.AddDomainEvent(new UpdateBookEvent(book.Id, book.Title, book.Description, request.AuthorIds, request.UserId));

            return await Commit(_efContext);
        }
    }
}
