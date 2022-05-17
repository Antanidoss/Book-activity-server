using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Filters;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IBookService
    {
        Task<ValidationResult> AddActiveBookAsync(CreateBookDTO createBookModel);
        Task<ValidationResult> RemoveActiveBookAsync(Guid bookId);
        Task<ValidationResult> UpdateActiveBookAsync(UpdateBookDTO updateBookModel);
        Task<IList<BookDTO>> GetByFilterAsync(BookDTOFilterModel filterModel);
    }
}