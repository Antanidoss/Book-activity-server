using AutoMapper;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Domain.Commands.BookNoteCommands.AddBookNote;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class BookNoteDTOProfile : Profile
    {
        public BookNoteDTOProfile()
        {
            CreateMap<CreateBookNoteDTO, AddBookNoteCommand>();
        }
    }
}
