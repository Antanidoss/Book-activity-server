using System.Threading;
using System.Threading.Tasks;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.ActiveBookEvents;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook
{
    internal sealed class AddActiveBookCommandHandler : CommandHandler,
        IRequestHandler<AddActiveBookCommand, ValidationResult>
    {
        private readonly IActiveBookRepository _activeBookRepository;

        private readonly IEventStoreRepository _eventStoreRepository;

        public AddActiveBookCommandHandler(IActiveBookRepository activeBookRepository, IEventStoreRepository eventStoreRepository)
        {
            _activeBookRepository = activeBookRepository;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<ValidationResult> Handle(AddActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBook activeBook = new(request.TotalNumberPages, request.NumberPagesRead, request.BookId, request.UserId);

            _activeBookRepository.Add(activeBook);

            request.Id = activeBook.Id;

            AddActiveBookEvent addActiveBookEvent = new(
                activeBook.Id,
                activeBook.TotalNumberPages,
                activeBook.NumberPagesRead,
                activeBook.BookId,
                activeBook.UserId);

            activeBook.AddDomainEvent(new AddActiveBookAfterOperationEvent(addActiveBookEvent));
            await _eventStoreRepository.SaveAsync(addActiveBookEvent);

            return await Commit(_activeBookRepository.UnitOfWork);
        }
    }
}