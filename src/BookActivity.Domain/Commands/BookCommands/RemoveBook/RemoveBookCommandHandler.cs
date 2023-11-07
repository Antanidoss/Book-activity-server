using BookActivity.Domain.Events.BookEvents;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Specifications.BookSpecs;
using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using BookActivity.Domain.Models;
using BookActivity.Domain.Filters.Models;

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
            DbSingleResultFilterModel<Book> filterModel = new(bookByIdSpec, forUpdate: false);
            var book = await _bookRepository.GetByFilterAsync(filterModel);

            book.AddDomainEvent(new RemoveBookEvent(book.Id, request.UserId));
            _bookRepository.Remove(book);

            return await Commit(_bookRepository.UnitOfWork);
        }
    }
}
