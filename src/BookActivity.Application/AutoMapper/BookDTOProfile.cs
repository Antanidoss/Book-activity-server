using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using BookActivity.Domain.Commands.BookCommands.UpdateBook;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class BookDtoProfile : Profile
    {
        public BookDtoProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();

            CreateMap<CreateBookDto, AddBookCommand>()
                .ForMember(b => b.ImageData, conf => conf.MapFrom(b => b.Image.ConvertToBuffer()));
            CreateMap<UpdateBookDTO, UpdateBookCommand>();
        }
    }
}