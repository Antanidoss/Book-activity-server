using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AuthorCommands.AddAuthor
{
    internal sealed class AddAuthorCommandHandler : CommandHandler,
        IRequestHandler<AddAuthorCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public AddAuthorCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            Author newAuthor = new(request.FirstName, request.Surname);

            await _efContext.Authors.AddAsync(newAuthor, cancellationToken);

            request.Id = newAuthor.Id;

            return await Commit(_efContext);
        }
    }
}
