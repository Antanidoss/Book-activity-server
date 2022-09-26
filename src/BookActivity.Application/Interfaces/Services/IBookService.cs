using Ardalis.Result;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Domain.Filters.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<ValidationResult> AddActiveBookAsync(CreateBookDto createBookModel);
        Task<ValidationResult> RemoveActiveBookAsync(Guid bookId);
        Task<ValidationResult> UpdateBookAsync(UpdateBookDto updateBookModel);
        Task<Result<IEnumerable<BookDto>>> GetByPaginationAsync(PaginationModel paginationModel, Guid currentUserId);
        Task<Result<IEnumerable<BookDto>>> GetByBookIdsAsync(Guid[] bookIds);
        Task<Result<IEnumerable<BookDto>>> GetByTitleContainsAsync(PaginationModel paginationModel, string title);
        Task<Result<IEnumerable<BookHistoryData>>> GetBookHistoryDataAsync(Guid bookId);
    }
}