using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser
{
    internal sealed class UpdateAppUserCommandHandler : CommandHandler,
        IRequestHandler<UpdateAppUserCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            return ValidationResult;
        }
    }
}
