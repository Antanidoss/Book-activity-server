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

namespace BookActivity.Domain.Commands.ActiveBookCommands.UpdateActiveBook
{
    internal sealed class UpdateActiveBookCommandHandler : CommandHandler,
        IRequestHandler<UpdateActiveBookCommand, ValidationResult>
    {
        private readonly IActiveBookRepository _activeBookRepository;

        public UpdateActiveBookCommandHandler(IActiveBookRepository activeBookRepository)
        {
            _activeBookRepository = activeBookRepository;
        }

        public async Task<ValidationResult> Handle(UpdateActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBookByIdSpec specification = new(request.Id);
            var activeBook = await _activeBookRepository.GetBySpecAsync(specification);

            if (activeBook is null)
                AddError(ValidationErrorMessage.GetEnitityNotFoundMessage(nameof(ActiveBook)));

            var prevNumberPagesRead = activeBook.NumberPagesRead;
            activeBook.NumberPagesRead = request.NumberPagesRead;

            _activeBookRepository.Update(activeBook);

            activeBook.AddDomainEvent(new UpdateActiveBookEvent(activeBook.Id, activeBook.NumberPagesRead, prevNumberPagesRead, request.UserId));

            return await Commit(_activeBookRepository.UnitOfWork).ConfigureAwait(false);
        }
    }
}
