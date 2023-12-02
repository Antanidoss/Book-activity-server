using AutoMapper;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class ActiveBookDtoProfile : Profile
    {
        public ActiveBookDtoProfile()
        {
            CreateMap<CreateActiveBookDto, AddActiveBookCommand>();
            CreateMap<UpdateNumberPagesReadDto, UpdateActiveBookCommand>()
                .ForMember(a => a.Id, conf => conf.MapFrom(a => a.ActiveBookId));
        }
    }
}
