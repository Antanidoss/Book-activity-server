using BookActivity.Domain.Filters.Models;
using BookActivity.Shared.Models;

namespace BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter
{
    public sealed class GetUsersByFilterQuery : Query<EntityListResult<SelectedAppUser>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Name { get; set; }
    }
}
