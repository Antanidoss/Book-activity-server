using Microsoft.AspNetCore.Http;

namespace BookActivity.Shared.Utils
{
    public static class HttpContextDataGetter
    {
        public static string GetConnectionId(IHttpContextAccessor httpContextAccessor) => httpContextAccessor.HttpContext.Request.Headers["ConnectionId"];
    }
}
