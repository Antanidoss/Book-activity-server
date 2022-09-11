using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.BookCommands;
using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class BookDTOProfile : Profile
    {
        public BookDTOProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();

            CreateMap<CreateBookDTO, AddBookCommand>();
            CreateMap<UpdateBookDTO, UpdateBookCommand>();
        }

        private byte[] IFromFileToBuffer(IFormFile file)
        {
            using MemoryStream stream = new();

            file.CopyTo(stream);

            return stream.ToArray();
        }
    }
}
