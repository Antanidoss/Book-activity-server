using AutoMapper;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    public sealed class ActiveBookDTOProfile : Profile
    {
        public ActiveBookDTOProfile()
        {
            CreateMap<ActiveBookDTO, ActiveBook>();
            CreateMap<ActiveBook, ActiveBookDTO>();
        }
    }
}
