using AutoMapper;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    public sealed class ActiveBookDTOProfile : Profile
    {
        public ActiveBookDTOProfile()
        {
            CreateMap<ActiveBookDTO, ActiveBook>();
            CreateMap<ActiveBook, ActiveBookDTO>();
            CreateMap<CreateActiveBookDTO, AddActiveBookCommand>();

            CreateMap<UpdateActiveBookDTO, UpdateActiveBookCommand>()
                .ForCtorParam("activeBookId", conf => conf.MapFrom(u => u.Id))
                .ForCtorParam("numberPagesRead", conf => conf.MapFrom(u => u.NumberPagesRead));
        }
    }
}
