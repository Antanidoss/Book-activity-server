using System;
using System.Linq;

namespace BookActivity.Domain.Validations
{
    public static class CommonValidator
    {
        public static void ThrowExceptionIfEmpty(Guid id, string paramName)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(paramName);
        }

        public static void ThrowExceptionIfNullOrEmpty<T>(T[] objects)
        {
            if (objects == null || !objects.Any())
                throw new ArgumentNullException($"{nameof(T)} cannot be null or empty");
        }

        public static void ThrowExceptionIfNull<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(T));
        }

        public static bool IsLessThanOrEqualToZero(int count)
        {
            return count <= 0;
        }
    }
}
