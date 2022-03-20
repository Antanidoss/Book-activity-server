using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Models.Filters;

namespace BookActivity.Application.Interfaces
{
    public interface IBaseService<R, C, U, F> : IBaseCommandService<C, U>, IBaseQueryService<R, F>
        where R : BaseEntityDTO
        where C : BaseCreateDTO
        where U : BaseUpdateDTO
        where F : BaseFilterModel
    {
    }
}