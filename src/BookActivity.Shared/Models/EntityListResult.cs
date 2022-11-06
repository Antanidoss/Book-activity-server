namespace BookActivity.Shared.Models
{
    public sealed class EntityListResult<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Entities { get; private set; }
        public int TotalCount { get; private set; }

        public EntityListResult(IEnumerable<TEntity> entityList, int totalCount)
        {
            Entities = entityList;
            TotalCount = totalCount;
        }

        public EntityListResult<TNewEntity> CopyWithNewEntityType<TNewEntity>(IEnumerable<TNewEntity> newEntities) where TNewEntity : class
        {
            return new EntityListResult<TNewEntity>(newEntities, TotalCount);
        }
    }
}
