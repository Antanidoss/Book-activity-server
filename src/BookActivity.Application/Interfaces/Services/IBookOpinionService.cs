using BookActivity.Application.Models.Dto.Update;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IBookOpinionService
    {
        Task<ValidationResult> AddBookOpinionAsync(AddBookOpinionDto addBookOpinionDto);
    }
}
