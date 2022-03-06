using System.Threading;
using System.Threading.Tasks;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace BookActivity.Domain.Commands.BookActiveCommands
{
    public class ActiveBookCommandHandler : CommandHandler,
        IRequestHandler<AddNewBookActiveCommand, ValidationResult>,
        IRequestHandler<UpdateActiveBookCommand, ValidationResult>,
        IRequestHandler<RemoveActiveBookCommand, ValidationResult>
    {
        private readonly IActiveBookRepository _activeBookRepository;

        public ActiveBookCommandHandler(IActiveBookRepository activeBookRepository)
        {
            _activeBookRepository = activeBookRepository;
        }

        public async Task<ValidationResult> Handle(AddNewBookActiveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var activeBook = new ActiveBook(request.TotalNumberPages, request.NumberPagesRead, request.BookId, request.UserId, request.IsPublic);
            _activeBookRepository.Add(activeBook);

            return await Commit(_activeBookRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var activeBook = await _activeBookRepository.GetByAsync(a => a.Id == request.Id);

            if (activeBook is null) AddError(ValidationErrorMessage.GetEnitityNotFoundMessage(nameof(ActiveBook)));

            activeBook.NumberPagesRead = request.NumberPagesRead;
            _activeBookRepository.Update(activeBook);

            return await Commit(_activeBookRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var activeBook = await _activeBookRepository.GetByAsync(a => a.Id == request.Id);
            if (activeBook is null) AddError(ValidationErrorMessage.GetEnitityNotFoundMessage(nameof(ActiveBook)));

            _activeBookRepository.Remove(activeBook);

            return await Commit(_activeBookRepository.UnitOfWork);
        }
    }
}