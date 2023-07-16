using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using BookActivity.Domain.Commands.BookCommands.UpdateBook;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class BookDtoProfile : Profile
    {
        public BookDtoProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<SelectedBook, BookDto>();
            CreateMap<UpdateBookDto, UpdateBookCommand>();

            CreateMap<CreateBookDto, AddBookCommand>()
                .ForMember(b => b.ImageData, conf => conf.MapFrom(b => b.Image.ConvertToBuffer()));
        }
    }
}