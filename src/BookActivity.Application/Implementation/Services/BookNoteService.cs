using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Domain.Commands.BookNoteCommands.AddBookNote;
using BookActivity.Domain.Interfaces;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class BookNoteService : IBookNoteService
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        public BookNoteService(IMapper mapper, IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<ValidationResult> AddBookNoteAsync(CreateBookNoteDto createBookNotemodel)
        {
            var addBookNoteCommand = _mapper.Map<AddBookNoteCommand>(createBookNotemodel);

            return await _mediatorHandler.SendCommand(addBookNoteCommand).ConfigureAwait(false);
        }
    }
}
