using AutoMapper;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class ActiveBookDTOProfile : Profile
    {
        public ActiveBookDTOProfile()
        {
            CreateMap<ActiveBookDTO, ActiveBook>();
            CreateMap<ActiveBook, ActiveBookDTO>()
                .ForMember(a => a.BookName, conf => conf.MapFrom(a => a.Book.Title))
                .ForMember(a => a.ImageData, conf => conf.MapFrom(a => a.Book.ImageData));

            CreateMap<CreateActiveBookDTO, AddActiveBookCommand>();

            CreateMap<UpdateNumberPagesReadDTO, UpdateActiveBookCommand>()
                .ForCtorParam("activeBookId", conf => conf.MapFrom(u => u.ActiveBookId))
                .ForCtorParam("numberPagesRead", conf => conf.MapFrom(u => u.NumberPagesRead));
        }
    }
}
