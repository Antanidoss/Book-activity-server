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

        protected async Task<ValidationResult> Commit(IDbContext dbContext, string message)
        {
            if (!(await dbContext.Commit()))
            {
                AddError(message);
            }

            return ValidationResult;
        }

        protected async Task<ValidationResult> Commit(IDbContext dbContext)
        {
            return await Commit(dbContext, "There was an error saving data").ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
