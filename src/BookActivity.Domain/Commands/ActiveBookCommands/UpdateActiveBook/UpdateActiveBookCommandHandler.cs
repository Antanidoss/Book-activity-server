using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Filters.Models;
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
        private readonly IEventStoreRepository _eventStoreRepository;

        public UpdateActiveBookCommandHandler(IActiveBookRepository activeBookRepository, IEventStoreRepository eventStoreRepository)
        {
            _activeBookRepository = activeBookRepository;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<ValidationResult> Handle(UpdateActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBookByIdSpec specification = new(request.Id);
            DbSingleResultFilterModel<ActiveBook> filterModel = new(specification, forUpdate: true);
            var activeBook = await _activeBookRepository.GetByFilterAsync(filterModel);

            if (activeBook is null)
                AddError(ValidationErrorConstants.GetEnitityNotFoundMessage(nameof(ActiveBook)));

            var prevNumberPagesRead = activeBook.NumberPagesRead;
            activeBook.NumberPagesRead = request.NumberPagesRead;

            UpdateActiveBookEvent updateActiveBookEvent = new(activeBook.Id, activeBook.NumberPagesRead, prevNumberPagesRead, request.UserId);
            activeBook.AddDomainEvent(updateActiveBookEvent);
            await _eventStoreRepository.SaveAsync(updateActiveBookEvent);

            return await Commit(_activeBookRepository.UnitOfWork);
        }
    }
}
