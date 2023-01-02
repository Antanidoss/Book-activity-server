using System.Threading;
using System.Threading.Tasks;
using BookActivity.Domain.Events.ActiveBookEvent;
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

        public AddActiveBookCommandHandler(IActiveBookRepository activeBookRepository)
        {
            _activeBookRepository = activeBookRepository;
        }

        public async Task<ValidationResult> Handle(AddActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBook activeBook = new(request.TotalNumberPages, request.NumberPagesRead, request.BookId, request.UserId);

            activeBook.AddDomainEvent(new AddActiveBookEvent(
                activeBook.Id,
                activeBook.TotalNumberPages,
                activeBook.NumberPagesRead,
                activeBook.BookId,
                activeBook.UserId));

            _activeBookRepository.Add(activeBook);

            request.Id = activeBook.Id;

            return await Commit(_activeBookRepository.UnitOfWork);
        }
    }
}