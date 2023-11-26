using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookOpinionCommads.AddBookOpinion
{
    internal sealed class AddBookOpinionCommandHandler : CommandHandler,
        IRequestHandler<AddBookOpinionCommand, ValidationResult>

    {
        private readonly IBookRepository _bookRepository;

        private readonly IBookOpinionRepository _bookOpinionRepository;

        public AddBookOpinionCommandHandler(IBookRepository bookRepository, IBookOpinionRepository bookOpinionRepository)
        {
            _bookRepository = bookRepository;
            _bookOpinionRepository = bookOpinionRepository;
        }

        public async Task<ValidationResult> Handle(AddBookOpinionCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookByIdSpec bookByIdSpec = new(request.BookId);
            DbSingleResultFilterModel<Book> filterModel = new(bookByIdSpec);
            var book = await _bookRepository.GetByFilterAsync(filterModel);

            if (book == null)
                throw new ArgumentException($"Could not be found book by id: {request.BookId}");

            BookOpinion bookOpinion = new(request.Grade, request.Descriptions, request.UserId, request.BookId);
            _bookOpinionRepository.Add(bookOpinion);

            return await Commit(_bookRepository.UnitOfWork);
        }
    }
}
