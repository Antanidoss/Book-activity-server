using BookActivity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IBaseRepository<BookAuthor>
    {
    }
}
