using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookOpinionCommads.RemoveDislike
{
    internal sealed class RemoveDislikeCommandHandler : CommandHandler,
        IRequestHandler<RemoveDislikeCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public RemoveDislikeCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(RemoveDislikeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookOpinionDislike dislike = new(request.BookId, request.UserIdOpinion, request.UserIdWhoDislike);
            _efContext.BookOpinionDislikes.Attach(dislike);
            _efContext.BookOpinionDislikes.Remove(dislike);

            return await Commit(_efContext);
        }
    }
}
