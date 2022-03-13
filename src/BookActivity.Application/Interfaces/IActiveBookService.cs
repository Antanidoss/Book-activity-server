using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.Filters;

namespace BookActivity.Application.Interfaces
{
    public interface IActiveBookService : IBaseService<ActiveBookDTO, ActiveBookFilterModel>
    {
       
    }
}
