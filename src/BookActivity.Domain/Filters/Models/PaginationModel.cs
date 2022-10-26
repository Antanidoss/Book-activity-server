namespace BookActivity.Domain.Filters.Models
{
    public class PaginationModel
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public const int SkipDefault = 0;
        public const int TakeDefault = 1;

        public PaginationModel()
        {

        }

        public PaginationModel(int? skip = SkipDefault, int? take = TakeDefault)
        {
            Skip = skip ?? SkipDefault;
            Take = take ?? TakeDefault;
        }
    }
}
