using Antanidoss.Specification.Interfaces;
using BookActivity.Infrastructure.Data.Exceptions;
using NetDevPack.Domain;

namespace BookActivity.Infrastructure.Data.Validations
{
    internal static class SpecificationValidator
    {
        public static void ThrowExceptionIfNull<TEntity>(ISpecification<TEntity> specification) where TEntity : class
        {
            if (specification == null)
                throw new SpecificationNullException(nameof(TEntity));
        } 
    }
}
