using Ardalis.Result;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter;
using BookActivity.Shared.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IActiveBookService
    {
        Task<Result<IEnumerable<ActiveBookDto>>> GetByActiveBookIdAsync(Guid[] activeBookIds);
        Task<Result<IEnumerable<ActiveBookDto>>> GetByUserIdAsync(PaginationModel paginationModel, Guid currentUserId);
        Task<Result<IEnumerable<ActiveBookHistoryData>>> GetActiveBookHistoryDataAsync(Guid activeBookId);
        Task<EntityListResult<SelectedActiveBook>> GetByFilterAsync(GetActiveBookByFilterQuery filterModel);
        Task<Result<Guid>> AddActiveBookAsync(CreateActiveBookDto createActiveBookModel);
        Task<ValidationResult> RemoveActiveBookAsync(Guid activeBookId);
        Task<ValidationResult> UpdateActiveBookAsync(UpdateNumberPagesReadDto updateActiveBookModel);
    }
}