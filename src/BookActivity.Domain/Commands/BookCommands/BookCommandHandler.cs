using Antanidoss.Specification.Filters.Implementation;
using BookActivity.Domain.Events.BookEvents;
using BookActivity.Domain.Exceptions;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AuthorSpecs;
using BookActivity.Domain.Specifications.BookSpecs;
using BookActivity.Domain.Vidations;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookCommands
{
    internal sealed class BookCommandHandler : CommandHandler,
        IRequestHandler<AddBookCommand, ValidationResult>,
        IRequestHandler<UpdateBookCommand, ValidationResult>,
        IRequestHandler<RemoveBookCommand, ValidationResult>
    {
        private readonly IBookRepository _bookRepository;

        private readonly IAuthorRepository _authorRepository;

        public BookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<ValidationResult> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookAuthorByIdSpec specification = new(request.AuthorIds.ToArray());
            Where<BookAuthor> filter = new(specification);
            var count = await _authorRepository.GetCountByFilterAsync(filter);

            if (CommonValidator.IsLessThanOrEqualToZero(count))
                throw new NotFoundException(nameof(request.AuthorIds));

            BookAuthorFilterModel authorFilterModel = new(filter, take: count);
            var authors = await _authorRepository.GetByFilterAsync(authorFilterModel);
            Book newBook = new(request.Title, request.Description, isPublic: true, authors.ToArray());

            newBook.AddDomainEvent(new AddBookEvent(newBook.Id, newBook.Title, newBook.Description, request.AuthorIds, newBook.IsPublic));
            _bookRepository.Add(newBook);

            return await Commit(_bookRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookByIdSpec bookByIdSpec = new(request.BookId);
            FirstOrDefault<Book> firstOrDefaultFilter = new(bookByIdSpec);
            var updatedBook = _bookRepository.GetByFilterAsync(firstOrDefaultFilter);

            updatedBook.Title = request.Title;
            updatedBook.Description = request.Description;

            updatedBook.AddDomainEvent(new UpdateBookEvent(updatedBook.Id, updatedBook.Description, request.AuthorIds, updatedBook.IsPublic));
            _bookRepository.Update(updatedBook);

            return await Commit(_bookRepository.UnitOfWork);
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

            return await Commit(_bookRepository.UnitOfWork);
        }
    }
}
