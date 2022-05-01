using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Update;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IBaseCommandService<TCreateEntity, TUpdateEntity>
        where TCreateEntity : BaseCreateDTO
        where TUpdateEntity : BaseUpdateDTO
    {
        Task<ValidationResult> AddActiveBookAsync(TCreateEntity createEntity);
        Task<ValidationResult> RemoveActiveBookAsync(Guid entityId);
        Task<ValidationResult> UpdateActiveBookAsync(TUpdateEntity updateEntity);
    }
}
