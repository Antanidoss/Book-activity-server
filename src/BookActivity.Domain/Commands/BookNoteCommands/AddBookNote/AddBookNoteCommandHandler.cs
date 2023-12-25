using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookNoteCommands.AddBookNote
{
    internal sealed class AddBookNoteCommandHandler : CommandHandler,
         IRequestHandler<AddBookNoteCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public AddBookNoteCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(AddBookNoteCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookNote newBookNote = new(request.Note, request.NoteColor, request.ActiveBookId, request.NoteTextColor);

            await _efContext.BookNotes.AddAsync(newBookNote);

            return await Commit(_efContext);
        }
    }
}
