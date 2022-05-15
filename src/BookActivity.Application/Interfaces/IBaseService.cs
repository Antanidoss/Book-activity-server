using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Filters;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;

namespace BookActivity.Application.Interfaces
{
    public interface IBaseService<TEntity, TCreateEntity, TUpdateEntity, TFilter> : IBaseCommandService<TCreateEntity, TUpdateEntity>, IBaseQueryService<TEntity, TFilter>
        where TEntity : BaseEntityDTO
        where TCreateEntity : BaseCreateDTO
        where TUpdateEntity : BaseUpdateDTO
        where TFilter : BaseDTOFilterModel
    {
    }
}