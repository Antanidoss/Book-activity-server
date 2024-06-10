using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.ActiveBookSpecs;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.ActiveBookCommands.UpdateActiveBook
{
    internal sealed class UpdateActiveBookCommandHandler : CommandHandler,
        IRequestHandler<UpdateActiveBookCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;
        private readonly IMongoDatabase _mongoContext;

        public UpdateActiveBookCommandHandler(IDbContext efContext, IMongoDatabase mongoContext)
        {
            _efContext = efContext;
            _mongoContext = mongoContext;
        }

        public async Task<ValidationResult> Handle(UpdateActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBookByIdSpec specification = new(request.Id);
            var activeBook = await _efContext.ActiveBooks.FirstOrDefaultAsync(specification, cancellationToken);

            if (activeBook is null)
                AddError(ValidationErrorConstants.GetEnitityNotFoundMessage(nameof(ActiveBook)));

            var prevNumberPagesRead = activeBook.NumberPagesRead;
            activeBook.NumberPagesRead = request.NumberPagesRead;

            UpdateActiveBookEvent updateActiveBookEvent = new(activeBook.Id, activeBook.NumberPagesRead, prevNumberPagesRead, request.UserId, activeBook.BookId);
            activeBook.AddDomainEvent(updateActiveBookEvent);
            await _mongoContext.GetCollection<UpdateActiveBookEvent>(EventMessageTypeConstants.UpdateActiveBook).InsertOneAsync(updateActiveBookEvent);

            return await Commit(_efContext);
        }
    }
}
