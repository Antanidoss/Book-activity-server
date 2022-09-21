using BookActivity.Application.Models.DTO.Create;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<ValidationResult> AddAsync(CreateAuthorDto createAuthor);
    }
}
