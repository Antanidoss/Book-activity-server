using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookOpinionCommads.AddBookOpinion
{
    internal sealed class AddBookOpinionCommandHandler : CommandHandler,
        IRequestHandler<AddBookOpinionCommand, ValidationResult>

    {
        private readonly IDbContext _efContext;

        public AddBookOpinionCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(AddBookOpinionCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookOpinion bookOpinion = new(request.Grade, request.Descriptions, request.UserId, request.BookId);
            await _efContext.BookOpinions.AddAsync(bookOpinion);

            return await Commit(_efContext);
        }
    }
}
