using System.Threading;
using System.Threading.Tasks;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.ActiveBookEvents;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using MongoDB.Driver;

namespace BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook
{
    internal sealed class AddActiveBookCommandHandler : CommandHandler,
        IRequestHandler<AddActiveBookCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;
        private readonly IMongoDatabase _mongoContext;

        public AddActiveBookCommandHandler(IDbContext efContext, IMongoDatabase mongoContext)
        {
            _efContext = efContext;
            _mongoContext = mongoContext;
        }

        public async Task<ValidationResult> Handle(AddActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBook activeBook = new(request.TotalNumberPages, request.NumberPagesRead, request.BookId, request.UserId);

            await _efContext.ActiveBooks.AddAsync(activeBook, cancellationToken);

            request.Id = activeBook.Id;

            AddActiveBookEvent addActiveBookEvent = new(
                activeBook.Id,
                activeBook.TotalNumberPages,
                activeBook.NumberPagesRead,
                activeBook.BookId,
                activeBook.UserId);

            activeBook.AddDomainEvent(new AddActiveBookAfterOperationEvent(addActiveBookEvent));
            await _mongoContext.GetCollection<AddActiveBookEvent>(EventMessageTypeConstants.AddActiveBook).InsertOneAsync(addActiveBookEvent);

            return await Commit(_efContext);
        }
    }
}