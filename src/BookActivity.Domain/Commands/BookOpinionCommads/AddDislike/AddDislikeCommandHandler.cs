using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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

            if (await _efContext.BookOpinions.Where(o => o.BookId == request.BookId).SelectMany(o => o.Likes).AnyAsync(l => l.UserIdWhoLike == request.UserIdWhoDislike))
                throw new Exception();

            await _efContext.BookOpinionLikes.AddAsync(new BookOpinionLike(request.BookId, request.UserIdOpinion, request.UserIdWhoDislike));

            return await Commit(_efContext);
        }
    }
}
