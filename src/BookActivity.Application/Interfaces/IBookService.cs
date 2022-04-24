using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Filters.Models;

namespace BookActivity.Application.Interfaces
{
    public interface IBookService : IBaseService<BookDTO, CreateBookDTO, UpdateBookDTO, BookFilterModel>
    {

    }
}