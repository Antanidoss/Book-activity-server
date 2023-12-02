using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using BookActivity.Domain.Commands.BookCommands.UpdateBook;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class BookDtoProfile : Profile
    {
        public BookDtoProfile()
        {
            CreateMap<UpdateBookDto, UpdateBookCommand>();

            CreateMap<CreateBookDto, AddBookCommand>()
                .ForMember(b => b.ImageData, conf => conf.MapFrom(b => b.Image.ConvertToBuffer()));
        }
    }
}
