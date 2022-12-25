using Antanidoss.Specification.Filters.Implementation;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.ActiveBookSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook
{
    internal sealed class RemoveActiveBookCommandHandler : CommandHandler,
        IRequestHandler<RemoveActiveBookCommand, ValidationResult>
    {
        private readonly IActiveBookRepository _activeBookRepository;

        public RemoveActiveBookCommandHandler(IActiveBookRepository activeBookRepository)
        {
            _activeBookRepository = activeBookRepository;
        }

        public async Task<ValidationResult> Handle(RemoveActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBookByIdSpec specification = new(request.Id);
            var activeBook = await _activeBookRepository.GetBySpecAsync(specification);

            if (activeBook is null)
                AddError(ValidationErrorConstants.GetEnitityNotFoundMessage(nameof(ActiveBook)));

            activeBook.AddDomainEvent(new RemoveActiveBookEvent(activeBook.Id));
            _activeBookRepository.Remove(activeBook);

            return await Commit(_activeBookRepository.UnitOfWork).ConfigureAwait(false);
        }
    }
}
