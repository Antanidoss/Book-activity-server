using AutoMapper;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class BookRatingDtoProfile : Profile
    {
        public BookRatingDtoProfile()
        {
            CreateMap<BookRating, BookRatingDto>()
                .ForMember(b => b.AverageRating, conf => conf.MapFrom(b => b.CalculateAverageRating()));
        }
    }
}
