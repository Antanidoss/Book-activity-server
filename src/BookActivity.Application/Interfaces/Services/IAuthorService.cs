﻿using Ardalis.Result;
using BookActivity.Application.Models.Dto.Create;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<Result<Guid>> AddAsync(CreateAuthorDto createAuthor);
    }
}
