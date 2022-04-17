using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IBaseQueryService<E, F>
        where E : BaseEntityDTO
        where F : BaseFilterModel
    {
        Task<IList<E>> GetByFilterAsync(F filterModel);
    }
}
