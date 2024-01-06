using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookOpinionCommads.AddDislike
{
    internal sealed class AddDislikeCommandHandler : CommandHandler,
        IRequestHandler<AddDislikeCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public AddDislikeCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(AddDislikeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            var like = await _efContext.BookOpinions
                .Where(o => o.BookId == request.BookId)
                .SelectMany(o => o.Likes)
                .FirstOrDefaultAsync(l => l.UserIdWhoLike == request.UserIdWhoDislike);

            if (like != null)
                _efContext.BookOpinionLikes.Remove(like);

            await _efContext.BookOpinionDislikes.AddAsync(new BookOpinionDislike(request.BookId, request.UserIdOpinion, request.UserIdWhoDislike));

            return await Commit(_efContext);
        }
    }
}
