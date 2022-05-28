using Ardalis.Result;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IAppUserService
    {
        Task<ValidationResult> AddAsync(AppUserCreateDTO appUserCreateDTO);
        Task<AppUserDTO> FindByIdAsync(Guid appUserId);
        Task<Result<AuthenticationResult>> PasswordSignInAsync(AuthenticationModel authenticationModel);
    }
}
