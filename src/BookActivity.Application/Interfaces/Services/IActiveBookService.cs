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
    public interface IActiveBookService
    {
        Task<ValidationResult> AddActiveBookAsync(CreateActiveBookDTO createActiveBookModel);
        Task<ValidationResult> RemoveActiveBookAsync(Guid activeBookId);
        Task<ValidationResult> UpdateActiveBookAsync(UpdateNumberPagesReadDTO updateActiveBookModel);
        Task<Result<IEnumerable<ActiveBookDTO>>> GetByActiveBookIdAsync(Guid[] activeBookIds);
        Task<Result<IEnumerable<ActiveBookDTO>>> GetByUserIdAsync(PaginationModel paginationModel, Guid userId);
        Task<Result<IEnumerable<ActiveBookHistoryData>>> GetActiveBookHistoryDataAsync(Guid activeBookId);
    }
}