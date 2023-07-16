using BookActivity.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    public class BaseController : Controller
    {
        protected CurrentUser _currentUser;

        public BaseController(CurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public BaseController() { }
    }
}
