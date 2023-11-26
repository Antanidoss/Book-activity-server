using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.BookOpinionCommads.AddBookOpinion;
using BookActivity.Domain.Interfaces;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class BookOpinionService : IBookOpinionService
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BookOpinionService(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<ValidationResult> AddBookOpinionAsync(AddBookOpinionDto addBookOpinionDto)
        {
            if (addBookOpinionDto == null)
                return new ValidationResult(new List<ValidationFailure> { new(nameof(addBookOpinionDto), "The update model cannot be null") });

            AddBookOpinionCommand updateBookRatingCommand = new()
            {
                BookId = addBookOpinionDto.BookId,
                Grade = addBookOpinionDto.Grade,
                Descriptions = addBookOpinionDto.Description,
                UserId = addBookOpinionDto.UserId,
            };

            return await _mediatorHandler.SendCommandAsync(updateBookRatingCommand);
        }
    }
}
