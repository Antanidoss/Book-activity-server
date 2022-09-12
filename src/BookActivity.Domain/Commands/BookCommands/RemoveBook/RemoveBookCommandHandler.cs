using Antanidoss.Specification.Filters.Implementation;
using BookActivity.Domain.Events.BookEvents;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Threading.Tasks;
using System.Threading;

namespace BookActivity.Domain.Commands.BookCommands.RemoveBook
{
    internal sealed class RemoveBookCommandHandler : CommandHandler,
        IRequestHandler<RemoveBookCommand, ValidationResult>
    {
        private readonly IBookRepository _bookRepository;

        public RemoveBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ValidationResult> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookByIdSpec bookByIdSpec = new(request.BookId);
            FirstOrDefault<Book> firstOrDefaultFilter = new(bookByIdSpec);
            var book = _bookRepository.GetByFilterAsync(firstOrDefaultFilter);

            book.AddDomainEvent(new RemoveBookEvent(book.Id));
            _bookRepository.Remove(book);

            return await Commit(_bookRepository.UnitOfWork).ConfigureAwait(false);
        }
    }
}
