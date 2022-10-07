namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class BookOpinionDto : BaseEntityDto
    {
        public int Grade { get; set; }
        public string Description { get; set; }
        public BookOpinionDto() : base() { }
    }
}