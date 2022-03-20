using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IBaseQueryService<R, F>
        where R : BaseEntityDTO
        where F : BaseFilterModel
    {
        Task<IList<R>> GetByFilterAsync(F filterModel);
        Task<R> GetByIdAsync(Guid entityId);
    }
}
