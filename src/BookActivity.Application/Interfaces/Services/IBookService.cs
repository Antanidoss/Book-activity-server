using Ardalis.Result;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Application.Models.HistoryData;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<ValidationResult> AddActiveBookAsync(CreateBookDto createBookModel);
        Task<ValidationResult> RemoveActiveBookAsync(Guid bookId, Guid userId);
        Task<ValidationResult> UpdateBookAsync(UpdateBookDto updateBookModel);
        Task<Result<IEnumerable<BookHistoryData>>> GetBookHistoryDataAsync(Guid bookId);
    }
}