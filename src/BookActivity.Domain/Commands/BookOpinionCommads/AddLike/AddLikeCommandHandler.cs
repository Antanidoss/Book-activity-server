using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookOpinionCommads.AddLike
{
    internal sealed class AddLikeCommandHandler : CommandHandler,
        IRequestHandler<AddLikeCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public AddLikeCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            if (await _efContext.BookOpinions.Where(o => o.BookId == request.BookId).SelectMany(o => o.Dislikes).AnyAsync(d => d.UserIdWhoDislike == request.UserIdWhoLike))
                throw new Exception();

            await _efContext.BookOpinionDislikes.AddAsync(new BookOpinionDislike(request.BookId, request.UserIdOpinion, request.UserIdWhoLike));

            return await Commit(_efContext);
        }
    }
}
