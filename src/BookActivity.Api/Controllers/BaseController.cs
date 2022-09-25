using BookActivity.Application.Models.DTO.Read;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected AppUserDto GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.Items["User"];

            return user != null ? (user as AppUserDto) : null;
        }
    }
}
