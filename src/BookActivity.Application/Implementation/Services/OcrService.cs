using BookActivity.Application.Extensions;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Queries.OcrQueries.GetTextOnImage;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class OcrService : IOcrService
    {
        private readonly IMediatorHandler _mediator;

        public OcrService(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> GetTextOnImageAsync(IFormFile image)
        {
            GetTextOnImageQuery query = new(image.ConvertToBuffer(), Path.GetExtension(image.FileName));

            return await _mediator.SendQueryAsync(query);
        }
    }
}
