using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using BookActivity.Domain.Models;
using BookActivity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookActivity.Domain.Commands.BookCommands.RemoveBook
{
    internal sealed class RemoveBookCommandHandler : CommandHandler,
        IRequestHandler<RemoveBookCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public RemoveBookCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            Book book = new() { Id = request.BookId };

            _efContext.Books.Attach(book);
            _efContext.Books.Remove(book);

            return await Commit(_efContext);
        }
    }
}
