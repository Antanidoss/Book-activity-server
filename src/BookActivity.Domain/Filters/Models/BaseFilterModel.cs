namespace BookActivity.Domain.Filters.Models
{
    public class BaseFilterModel
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        protected const int _skip = 0;
        protected const int _take = 1;

        public BaseFilterModel(int skip = _skip, int take = _take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
