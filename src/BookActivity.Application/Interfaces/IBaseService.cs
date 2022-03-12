using BookActivity.Application.Models.DTO;
using BookActivity.Application.Models.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IBaseService<E, F> 
        where E : BaseEntityDTO
        where F : BaseFilterModel
    {
        Task<ValidationResult> AddActiveBookAsync(E entity);
        Task<ValidationResult> RemoveActiveBookAsync(Guid entityId);
        Task<ValidationResult> UpdateActiveBookAsync(E entity);

        Task<IList<E>> GetByFilterAsync(F filterModel);
        Task<E> GetByIdAsync(Guid entityId);
    }
}
