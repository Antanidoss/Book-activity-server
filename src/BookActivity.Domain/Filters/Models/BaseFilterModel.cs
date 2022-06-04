namespace BookActivity.Domain.Filters.Models
{
    public class BaseFilterModel
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public const int SkipDefault = 0;
        public const int TakeDefault = 1;

        public BaseFilterModel(int skip = SkipDefault, int take = TakeDefault)
        {
            Skip = skip;
            Take = take;
        }
    }
}
