using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Intefaces
{
    public interface IDbInitializer
    {
        Task InitializeAsync(BookActivityContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager);
    }
}
