using BookActivity.Application.Models;
using System;
using System.Linq;

namespace BookActivity.Application.Vidations
{
    internal static class CommonValidator
    {
        public static void ThrowExceptionIfNull(PaginationModel paginationModel)
        {
            if (paginationModel == null)
                throw new ArgumentNullException($"{nameof(PaginationModel)} cannot be null");
        }

        public static void ThrowExceptionIfEmpty(Guid id, string paramName)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException($"{paramName} cannot be null");
        }

        public static void ThrowExceptionIfNullOrEmpty<T>(T[] objects, string paramName)
        {
            if (objects == null || !objects.Any())
                throw new ArgumentNullException($"{paramName} cannot be null or empty");
        }
    }
}
