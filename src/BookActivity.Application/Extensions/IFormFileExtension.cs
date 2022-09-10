using Microsoft.AspNetCore.Http;
using System.IO;

namespace BookActivity.Application.Extensions
{
    public static class IFormFileExtension
    {
        public static byte[] ConvertToBuffer(this IFormFile file)
        {
            if (file.Length == 0)
                return null;

            using var ms = new MemoryStream();
            file.CopyTo(ms);

            return ms.ToArray();
        }
    }
}
