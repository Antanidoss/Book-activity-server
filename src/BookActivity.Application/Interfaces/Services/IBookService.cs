using Ardalis.Result;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
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
        Task<Result<IEnumerable<BookDTO>>> GetByBookIdsFilterAsync(PaginationModel paginationModel, Guid[] bookIds);
        Task<Result<IEnumerable<BookDTO>>> GetByTitleContainsFilterAsync(PaginationModel paginationModel, string title);
    }
}