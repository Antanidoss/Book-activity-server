namespace BookActivity.Domain.Queries.OcrQueries.GetTextOnImage
{
    public class GetTextOnImageQuery : Query<string>
    {
        public readonly byte[] ImageContent;

        public readonly string ImageExtension;

        public GetTextOnImageQuery(byte[] imageContent, string imageExtension)
        {
            ImageContent = imageContent;
            ImageExtension = imageExtension;
        }
    }
}
