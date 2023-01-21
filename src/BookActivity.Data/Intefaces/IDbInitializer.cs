using BookActivity.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Intefaces
{
    public interface IDbInitializer
    {
        Task InitializeAsync(BookActivityContext context);
    }
}
