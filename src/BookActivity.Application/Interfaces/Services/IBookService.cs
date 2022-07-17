using Ardalis.Result;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Domain.FilterModels;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<ValidationResult> AddActiveBookAsync(CreateBookDTO createBookModel);
        Task<ValidationResult> RemoveActiveBookAsync(Guid bookId);
        Task<ValidationResult> UpdateBookAsync(UpdateBookDTO updateBookModel);
        Task<Result<IEnumerable<BookDTO>>> GetByBookIdsAsync(Guid[] bookIds);
        Task<Result<IEnumerable<BookDTO>>> GetByTitleContainsAsync(PaginationModel paginationModel, string title);
        Task<Result<IEnumerable<BookHistoryData>>> GetBookHistoryDataAsync(Guid bookId);
    }
}