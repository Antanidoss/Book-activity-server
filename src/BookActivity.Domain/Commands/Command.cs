using FluentValidation.Results;
using MediatR;

namespace BookActivity.Domain.Commands
{
    public abstract class Command : IRequest<ValidationResult>, IBaseRequest
    {
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
