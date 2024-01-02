using BookActivity.Application.Models.Dto.Update;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IBookOpinionService
    {
        Task<ValidationResult> AddBookOpinionAsync(AddBookOpinionDto addBookOpinionDto);
        Task<ValidationResult> AddDislikeAsync(Guid bookId, Guid userIdWhoDislike, Guid userIdOpinion);
        Task<ValidationResult> AddLikeAsync(Guid bookId, Guid userIdWhoLike, Guid userIdOpinion);
        Task<ValidationResult> RemoveDislikeAsync(Guid bookId, Guid userIdWhoDislike, Guid userIdOpinion);
        Task<ValidationResult> RemoveLikeAsync(Guid bookId, Guid userIdWhoLike, Guid userIdOpinion);
    }
}
