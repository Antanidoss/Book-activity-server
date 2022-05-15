using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Filters;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;

namespace BookActivity.Application.Interfaces
{
    public interface IActiveBookService : IBaseService<ActiveBookDTO, CreateActiveBookDTO, UpdateActiveBookDTO, ActiveBookDTOFilterModel>
    {
    }
}