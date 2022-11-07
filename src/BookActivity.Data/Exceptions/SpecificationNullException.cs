using System;

namespace BookActivity.Infrastructure.Data.Exceptions
{
    internal sealed class SpecificationNullException : Exception
    {
        public SpecificationNullException(string nameofEntitySpec) : base($"Specification cannot be null. Specification entity: ${nameofEntitySpec}")
        {

        }
    }
}
