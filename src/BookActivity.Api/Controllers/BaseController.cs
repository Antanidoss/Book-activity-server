using BookActivity.Application.Models.Dto.Read;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    public class BaseController : Controller
    {
        protected AppUserDto _currentUser;

        public BaseController(AppUserDto currentUser)
        {
            _currentUser = currentUser;
        }

        public BaseController() { }
    }
}
