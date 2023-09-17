using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IOcrService
    {
        public Task<string> GetTextOnImageAsync(IFormFile image);
    }
}
