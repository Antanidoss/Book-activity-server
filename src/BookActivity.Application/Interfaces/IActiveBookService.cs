using BookActivity.Application.Models.DTO;
using BookActivity.Application.Models.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IActiveBookService : IBaseService<ActiveBookDTO, ActiveBookFilterModel>
    {
       
    }
}
