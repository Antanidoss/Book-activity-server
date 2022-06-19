using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.Specifications.AuthorSpecs;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
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
            if (!request.IsValid()) return request.ValidationResult;

            BookAuthorFilterModel authorFilterModel = new(new BookAuthorByIdSpec(request.AuthorIds.ToArray()));
            authorFilterModel.Take = await _authorRepository.GetCountByFilterAsync(authorFilterModel);

            var authors = await _authorRepository.GetByFilterAsync(authorFilterModel);
            Book newBook = new(request.Title, request.Description, isPublic: true, authors.ToArray());

            _bookRepository.Add(newBook);

            return await Commit(_bookRepository.UnitOfWork);
        }

        public Task<ValidationResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
