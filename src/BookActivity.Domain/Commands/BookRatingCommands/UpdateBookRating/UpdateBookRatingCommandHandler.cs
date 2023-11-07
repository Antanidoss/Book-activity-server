using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookRatingSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookRatingCommands.UpdateBookRating
{
    internal sealed class UpdateBookRatingCommandHandler : CommandHandler,
        IRequestHandler<UpdateBookRatingCommand, ValidationResult>

    {
        private readonly IBookRatingRepository _bookRatingRepository;

        private readonly IBookOpinionRepository _bookOpinionRepository;

        public UpdateBookRatingCommandHandler(IBookRatingRepository bookRatingRepository, IBookOpinionRepository bookOpinionRepository)
        {
            _bookRatingRepository = bookRatingRepository;
            _bookOpinionRepository = bookOpinionRepository;
        }

        public async Task<ValidationResult> Handle(UpdateBookRatingCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookRatingByIdSpec bookRatingByIdSpec = new(request.Id);
            DbSingleResultFilterModel<BookRating> filterModel = new(bookRatingByIdSpec);
            var bookRating = await _bookRatingRepository.GetByFilterAsync(filterModel);

            if (bookRating == null)
                throw new ArgumentException($"Could not be found book rating by id: {request.Id}");

            if (bookRating.BookOpinions == null || !bookRating.BookOpinions.Any())
                bookRating.BookOpinions = new List<BookOpinion>();
            else if (bookRating.BookOpinions.Any(o => o.UserId == request.BookOpinion.UserId))
                return new ValidationResult(new List<ValidationFailure> { new(nameof(request.BookOpinion), "This user has already rated the book") });

            request.BookOpinion.BookRatingId = bookRating.Id;

            _bookOpinionRepository.Add(request.BookOpinion);

            return await Commit(_bookRatingRepository.UnitOfWork);
        }
    }
}
