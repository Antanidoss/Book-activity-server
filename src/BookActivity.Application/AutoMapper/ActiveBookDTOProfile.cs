using AutoMapper;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class ActiveBookDtoProfile : Profile
    {
        public ActiveBookDtoProfile()
        {
            CreateMap<ActiveBookDto, ActiveBook>();
            CreateMap<ActiveBook, ActiveBookDto>();
            CreateMap<CreateActiveBookDto, AddActiveBookCommand>();
            CreateMap<UpdateNumberPagesReadDto, UpdateActiveBookCommand>()
                .ForMember(a => a.Id, conf => conf.MapFrom(a => a.ActiveBookId));
        }
    }
}
