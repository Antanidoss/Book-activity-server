using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookOpinionCommads.RemoveLike
{
    internal sealed class RemoveLikeCommandHandler : CommandHandler,
        IRequestHandler<RemoveLikeCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public RemoveLikeCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookOpinionLike like = new(request.BookId, request.UserIdOpinion, request.UserIdWhoLike);
            _efContext.BookOpinionLikes.Attach(like);
            _efContext.BookOpinionLikes.Remove(like);

            return await Commit(_efContext);
        }
    }
}
