using BookActivity.Application.Models.DTO.Create;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces
{
    public interface IBookNoteService
    {
        Task<ValidationResult> AddBookNote(CreateBookNoteDTO createBookNotemodel);
    }
}
