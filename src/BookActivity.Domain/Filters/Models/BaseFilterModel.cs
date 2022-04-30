namespace BookActivity.Domain.Filters.Models
{
    public class BaseFilterModel
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public BaseFilterModel(int skip = 0, int take = 0)
        {
            Skip = skip;
            Take = take;
        }
    }
}
