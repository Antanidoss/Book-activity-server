using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.OcrService)]
    public class OcrController : BaseController
    {
        private readonly IOcrService _ocrService;

        public OcrController(IOcrService ocrService)
        {
            _ocrService = ocrService;
        }

        [HttpPost(ApiConstants.GetTextOnImageMethod)]
        public async Task<ApiResult<string>> GetTextOnImageAsync(IFormFile image)
        {
            return (await _ocrService.GetTextOnImageAsync(image)).ToApiResult();
        }
    }
}