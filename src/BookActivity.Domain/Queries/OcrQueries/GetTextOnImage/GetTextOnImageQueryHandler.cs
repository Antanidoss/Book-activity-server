using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TesseractOCR.Enums;
using TesseractOCR;
using System.IO;
using BookActivity.Shared;

namespace BookActivity.Domain.Queries.OcrQueries.GetTextOnImage
{
    internal sealed class GetTextOnImageQueryHandler : IRequestHandler<GetTextOnImageQuery, string>
    {
        public async Task<string> Handle(GetTextOnImageQuery request, CancellationToken cancellationToken)
        {
            var tempImagePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            try
            {
                tempImagePath = Path.ChangeExtension(tempImagePath, request.ImageExtension);
                await File.WriteAllBytesAsync(tempImagePath, request.ImageContent);

                using var engine = new Engine(Path.Combine(AssemblyHelper.AssemblyDirectory, "tessdata"), Language.Russian, EngineMode.Default);
                using var img = TesseractOCR.Pix.Image.LoadFromFile(tempImagePath);
                using var page = engine.Process(img);

                return page.Text;
            }
            finally
            {
                File.Delete(tempImagePath);
            }
        }
    }
}
