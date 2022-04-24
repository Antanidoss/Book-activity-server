using BookActivity.Application.Models.DTO.Read;
using BookActivity.Domain.Filters.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IBaseQueryService<TEntity, TFilter>
        where TEntity : BaseEntityDTO
        where TFilter : BaseFilterModel
    {
        Task<IList<TEntity>> GetByFilterAsync(TFilter filterModel);
    }
}
