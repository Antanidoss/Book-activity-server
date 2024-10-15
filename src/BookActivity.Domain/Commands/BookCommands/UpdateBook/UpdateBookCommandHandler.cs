using BookActivity.Domain.Events.BookEvents;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Specifications.BookSpecs;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
            var book = await _efContext.Books.FirstOrDefaultAsync(bookByIdSpec);

            if (book == null)
                throw new Exception();

            if (!string.IsNullOrEmpty(request.Title))
                book.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Description))
                book.Description = request.Description;
            if (request.ImageData != null && request.ImageData.Any())
                book.ImageData = request.ImageData;

            book.AddDomainEvent(new UpdateBookEvent(book.Id, book.Title, book.Description, request.AuthorIds, request.UserId));

            return await Commit(_efContext);
        }
    }
}
