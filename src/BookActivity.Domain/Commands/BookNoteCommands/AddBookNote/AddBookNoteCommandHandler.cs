using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookNoteCommands.AddBookNote
{
    internal sealed class AddBookNoteCommandHandler : CommandHandler,
         IRequestHandler<AddBookNoteCommand, ValidationResult>
    {
        private readonly IBookNoteRepository _bookNoteRepository;

        public AddBookNoteCommandHandler(IBookNoteRepository bookNoteRepository)
        {
            _bookNoteRepository = bookNoteRepository;
        }

        public async Task<ValidationResult> Handle(AddBookNoteCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            BookNote newBookNote = new(request.Note, request.NoteColor, request.ActiveBookId);

            _bookNoteRepository.Add(newBookNote);

            return await Commit(_bookNoteRepository.UnitOfWork);
        }
    }
}
