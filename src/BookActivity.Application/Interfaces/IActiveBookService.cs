using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Models.Filters;

namespace BookActivity.Application.Interfaces
{
    public interface IActiveBookService : IBaseService<ActiveBookDTO, CreateActiveBookDTO, UpdateActiveBookDTO, ActiveBookFilterModel>
    {
    }
}