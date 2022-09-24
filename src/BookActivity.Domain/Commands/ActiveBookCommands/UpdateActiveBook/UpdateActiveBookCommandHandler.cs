﻿using Antanidoss.Specification.Filters.Implementation;
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
            FirstOrDefault<ActiveBook> filter = new(specification);
            var activeBook = _activeBookRepository.GetByFilter(filter);

            if (activeBook is null)
                AddError(ValidationErrorMessage.GetEnitityNotFoundMessage(nameof(ActiveBook)));

            activeBook.NumberPagesRead = request.NumberPagesRead;

            activeBook.AddDomainEvent(new UpdateActiveBookEvent(activeBook.Id, activeBook.NumberPagesRead));
            _activeBookRepository.Update(activeBook);

            return await Commit(_activeBookRepository.UnitOfWork).ConfigureAwait(false);
        }
    }
}