using Ardalis.Result;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.DTO.Create;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<Result<Guid>> AddAsync(CreateAuthorDto createAuthor);
        Task<Result<IEnumerable<AuthorDto>>> GetAuthorsByNameAsync(string name, int take);
    }
}
