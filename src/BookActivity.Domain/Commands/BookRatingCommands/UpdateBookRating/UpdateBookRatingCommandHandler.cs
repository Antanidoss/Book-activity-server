using Antanidoss.Specification.Filters.Implementation;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookRatingSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
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

        public UpdateBookRatingCommandHandler(IBookRatingRepository bookRatingRepository)
        {
            _bookRatingRepository = bookRatingRepository;
        }

        public async Task<ValidationResult> Handle(UpdateBookRatingCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookRatingByIdSpec bookRatingByIdSpec = new(request.Id);
            FirstOrDefault<BookRating> BookRating = new(bookRatingByIdSpec);
            var bookRating = _bookRatingRepository.GetByFilterAsync(BookRating);

            if (bookRating.BookOpinions.Any(o => o.UserId == request.BookOpinion.UserId))
                return new ValidationResult(new List<ValidationFailure> { new(nameof(request.BookOpinion), "This user has already rated the book") });

            bookRating.BookOpinions.Add(request.BookOpinion);

            return await Commit(_bookRatingRepository.UnitOfWork);
        }
    }
}
