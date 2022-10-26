namespace BookActivity.Domain.Filters.Models
{
    public sealed class BookFilterModel
    {
        public string BookTitle { get; set; }
        public float AverageRatingFrom { get; set; }
        public float AverageRatingTo { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
