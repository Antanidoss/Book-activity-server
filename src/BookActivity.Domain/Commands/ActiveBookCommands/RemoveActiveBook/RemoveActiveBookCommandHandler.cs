using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook
{
    internal sealed class RemoveActiveBookCommandHandler : CommandHandler,
        IRequestHandler<RemoveActiveBookCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public RemoveActiveBookCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(RemoveActiveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            ActiveBook activeBook = new() { Id = request.Id };
            _efContext.ActiveBooks.Attach(activeBook);
            _efContext.ActiveBooks.Remove(activeBook);

            RemoveActiveBookEvent removeActiveBookEvent = new(activeBook.Id, request.UserId);
            activeBook.AddDomainEvent(removeActiveBookEvent);

            return await Commit(_efContext);
        }
    }
}
