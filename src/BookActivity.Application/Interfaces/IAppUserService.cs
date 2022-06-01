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
        Task<ValidationResult> SubscribeAppUserCommand(Guid currentUserId, Guid subscribedUserId);
        Task<Result<AuthenticationResult>> PasswordSignInAsync(AuthenticationModel authenticationModel);
        Task<AppUserDTO> FindByIdAsync(Guid appUserId);
    }
}
