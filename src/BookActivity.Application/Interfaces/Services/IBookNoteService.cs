using BookActivity.Application.Models.Dto.Create;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IBookNoteService
    {
        Task<ValidationResult> AddBookNoteAsync(CreateBookNoteDto createBookNotemodel);
    }
}
