namespace BookActivity.Application.Models.Filters
{
    public class BaseFilterModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public BaseFilterModel(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
