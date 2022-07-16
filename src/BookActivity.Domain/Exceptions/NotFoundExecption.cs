using System;

namespace BookActivity.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName) : base($"Couldn't find {entityName}")
        {

        }
    }
}
