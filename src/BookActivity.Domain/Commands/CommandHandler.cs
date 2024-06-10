using BookActivity.Domain.Interfaces;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> Commit(IDbContext dbContext)
        {
            if (!(await dbContext.Commit()))
            {
                AddError("There was an error saving data");
            }

            return ValidationResult;
        }
    }
}
