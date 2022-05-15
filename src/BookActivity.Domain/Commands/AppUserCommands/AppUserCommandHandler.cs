using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands
{
    public sealed class AppUserCommandHandler : CommandHandler,
        IRequestHandler<AddAppUserCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(AddAppUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
