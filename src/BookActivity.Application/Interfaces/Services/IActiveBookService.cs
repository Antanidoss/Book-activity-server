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
    public interface IActiveBookService
    {
        Task<Result<Guid>> AddActiveBookAsync(CreateActiveBookDto createActiveBookModel);
        Task<ValidationResult> RemoveActiveBookAsync(Guid activeBookId);
        Task<ValidationResult> UpdateActiveBookAsync(UpdateNumberPagesReadDto updateActiveBookModel);
    }
}