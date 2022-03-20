using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IBaseCommandService<C, U>
        where C : BaseCreateDTO
        where U : BaseUpdateDTO
    {
        Task<ValidationResult> AddActiveBookAsync(C entity);
        Task<ValidationResult> RemoveActiveBookAsync(Guid entityId);
        Task<ValidationResult> UpdateActiveBookAsync(U entity);
    }
}
