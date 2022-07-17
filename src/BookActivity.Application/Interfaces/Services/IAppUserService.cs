using Ardalis.Result;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IAppUserService
    {
        Task<ValidationResult> AddAsync(AppUserCreateDTO appUserCreateDTO);
        Task<ValidationResult> SubscribeAppUserAsync(Guid currentUserId, Guid subscribedUserId);
        Task<Result<AuthenticationResult>> PasswordSignInAsync(AuthenticationModel authenticationModel);
        Task<Result<AppUserDTO>> FindByIdAsync(Guid appUserId);
    }
}
