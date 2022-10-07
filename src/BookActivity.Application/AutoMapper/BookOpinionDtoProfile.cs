using AutoMapper;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal class BookOpinionDtoProfile : Profile
    {
        public BookOpinionDtoProfile()
        {
            CreateMap<CreateBookOpinionDto, BookOpinion>();
            CreateMap<BookOpinion, BookOpinionDto>();
        }
    }
}
