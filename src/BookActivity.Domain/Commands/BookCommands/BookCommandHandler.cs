using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookCommands
{
    public class BookCommandHandler : CommandHandler,
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

            List<BookAuthor> authors = new();
            foreach (var authorId in request.AuthorIds)
            {
                authors.Add(await _authorRepository.GetByAsync(a => authorId == a.Id));
            }

            var author = await _authorRepository.GetByAsync(a => request.AuthorIds.Any(i => i == a.Id));
            var newBook = new Book(request.Title, request.Description, true, authors.ToArray());

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
