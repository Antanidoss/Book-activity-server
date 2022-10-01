using AutoMapper;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal class BookOpinionDtoProfile : Profile
    {
        public BookOpinionDtoProfile()
        {
            CreateMap<CreateBookOpinionDto, BookOpinion>();
        }
    }
}
