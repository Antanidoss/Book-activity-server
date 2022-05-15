using BookActivity.Application.Models.DTO.Filters;
using BookActivity.Application.Models.DTO.Read;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IBaseQueryService<TEntity, TFilter>
        where TEntity : BaseEntityDTO
        where TFilter : BaseDTOFilterModel
    {
        Task<IList<TEntity>> GetByFilterAsync(TFilter filterModel);
    }
}
