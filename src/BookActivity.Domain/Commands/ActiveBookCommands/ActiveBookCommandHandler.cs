using System;
using System.Threading;
using System.Threading.Tasks;
using Antanidoss.Specification.Filters.Implementation;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.ActiveBookSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace BookActivity.Domain.Commands.ActiveBookCommands
{
    internal sealed class ActiveBookCommandHandler : CommandHandler,
        IRequestHandler<AddActiveBookCommand, ValidationResult>,
        IRequestHandler<UpdateActiveBookCommand, ValidationResult>,
        IRequestHandler<RemoveActiveBookCommand, ValidationResult>
    {
        private readonly IActiveBookRepository _activeBookRepository;

        public ActiveBookCommandHandler(IActiveBookRepository activeBookRepository)
        {
            _activeBookRepository = activeBookRepository;
        }

        public async Task<ValidationResult> Handle(AddActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBook activeBook = new(Guid.NewGuid(), request.TotalNumberPages, request.NumberPagesRead, request.BookId, request.UserId, request.IsPublic);

            activeBook.AddDomainEvent(new AddActiveBookEvent(
                activeBook.Id,
                activeBook.TotalNumberPages,
                activeBook.NumberPagesRead,
                activeBook.BookId,
                activeBook.UserId,
                activeBook.IsPublic));

            _activeBookRepository.Add(activeBook);

            return await Commit(_activeBookRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBookByIdSpec specification = new(request.Id);
            FirstOrDefault<ActiveBook> filter = new(specification);
            var activeBook = _activeBookRepository.GetByFilterAsync(filter);

            if (activeBook is null)
                AddError(ValidationErrorMessage.GetEnitityNotFoundMessage(nameof(ActiveBook)));

            activeBook.NumberPagesRead = request.NumberPagesRead;

            activeBook.AddDomainEvent(new UpdateActiveBookEvent(activeBook.Id, activeBook.NumberPagesRead));
            _activeBookRepository.Update(activeBook);

            return await Commit(_activeBookRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBookByIdSpec specification = new(request.Id);
            FirstOrDefault<ActiveBook> filter = new(specification);
            var activeBook = _activeBookRepository.GetByFilterAsync(filter);

            if (activeBook is null)
                AddError(ValidationErrorMessage.GetEnitityNotFoundMessage(nameof(ActiveBook)));

            activeBook.AddDomainEvent(new RemoveActiveBookEvent(activeBook.Id));
            _activeBookRepository.Remove(activeBook);

            return await Commit(_activeBookRepository.UnitOfWork);
        }
    }
}