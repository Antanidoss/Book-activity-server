using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Domain.Commands.BookNoteCommands.AddBookNote;
using FluentValidation.Results;
using NetDevPack.Mediator;
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

        public async Task<ValidationResult> AddBookNoteAsync(CreateBookNoteDTO createBookNotemodel)
        {
            var addBookNoteCommand = _mapper.Map<AddBookNoteCommand>(createBookNotemodel);

            return await _mediatorHandler.SendCommand(addBookNoteCommand).ConfigureAwait(false);
        }
    }
}
