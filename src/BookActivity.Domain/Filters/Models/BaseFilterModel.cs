namespace BookActivity.Domain.Filters.Models
{
    public class BaseFilterModel
    {
        public readonly int Skip;

        public readonly int Take;

        public BaseFilterModel(int skip = 0, int take = 0)
        {
            Skip = skip;
            Take = take;
        }
    }
}
