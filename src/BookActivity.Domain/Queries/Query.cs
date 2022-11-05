using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Queries
{
    public class Query<TResult> : Message, IRequest<TResult>, IBaseRequest
    {
        public DateTime Timestamp { get; private set; }

        public ValidationResult ValidationResult { get; set; }

        protected Query()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
