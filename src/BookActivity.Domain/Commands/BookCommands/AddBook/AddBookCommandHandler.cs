using BookActivity.Domain.Events.BookEvents;
using BookActivity.Domain.Exceptions;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AuthorSpecs;
using BookActivity.Domain.Validations;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookCommands.AddBook
{
    internal sealed class AddBookCommandHandler : CommandHandler,
        IRequestHandler<AddBookCommand, ValidationResult>
    {
        private readonly IBookRepository _bookRepository;

        private readonly IAuthorRepository _authorRepository;

        public AddBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<ValidationResult> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            AuthorByIdSpec specification = new(request.AuthorIds.ToArray());
            var authorCount = await _authorRepository.GetCountByFilterAsync(specification);

            if (CommonValidator.IsLessThanOrEqualToZero(authorCount))
                throw new NotFoundException(nameof(request.AuthorIds));

            var bookAuthor = request.AuthorIds.Select(a => new BookAuthor { AuthorId = a });
            Book newBook = new(request.Title, request.Description, request.ImageData, bookAuthor);

            newBook.AddDomainEvent(new AddBookEvent(newBook.Id, newBook.Title, newBook.Description, request.AuthorIds));
            _bookRepository.Add(newBook);

            return await Commit(_bookRepository.UnitOfWork);
        }
    }
}
