using System;
using System.Threading;
using System.Threading.Tasks;
using BookActivity.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace BookActivity.Domain.Commands.BookActiveCommands
{
    public class ActiveBookHandler : CommandHandler,
        IRequestHandler<AddNewBookActiveCommand, ValidationResult>,
        IRequestHandler<UpdateActiveBookCommand, ValidationResult>,
        IRequestHandler<RemoveActiveBookCommand, ValidationResult>
    {
        private readonly IActiveBookRepository _activeBookRepository;

        public ActiveBookHandler(IActiveBookRepository activeBookRepository)
        {
            _activeBookRepository = activeBookRepository;
        }

        public Task<ValidationResult> Handle(AddNewBookActiveCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> Handle(UpdateActiveBookCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> Handle(RemoveActiveBookCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}