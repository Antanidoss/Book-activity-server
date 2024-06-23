using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.BookOpinionCommads.AddBookOpinion;
using BookActivity.Domain.Commands.BookOpinionCommads.AddDislike;
using BookActivity.Domain.Commands.BookOpinionCommads.AddLike;
using BookActivity.Domain.Commands.BookOpinionCommads.RemoveDislike;
using BookActivity.Domain.Commands.BookOpinionCommads.RemoveLike;
using BookActivity.Domain.Interfaces;
using FluentValidation.Results;
using System;
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

        public async Task<ValidationResult> AddDislikeAsync(Guid bookId, Guid userIdWhoDislike, Guid userOpinionId)
        {
            AddDislikeCommand command = new(userIdWhoDislike, bookId, userOpinionId);

            return await _mediatorHandler.SendCommandAsync(command);
        }

        public async Task<ValidationResult> AddLikeAsync(Guid bookId, Guid userIdWhoLike, Guid userOpinionId)
        {
            AddLikeCommand command = new(userIdWhoLike, bookId, userOpinionId);

            return await _mediatorHandler.SendCommandAsync(command);
        }

        public async Task<ValidationResult> RemoveDislikeAsync(Guid bookId, Guid userIdWhoDislike, Guid userOpinionId)
        {
            RemoveDislikeCommand command = new(userIdWhoDislike, bookId, userOpinionId);

            return await _mediatorHandler.SendCommandAsync(command);
        }

        public async Task<ValidationResult> RemoveLikeAsync(Guid bookId, Guid userIdWhoLike, Guid userOpinionId)
        {
            RemoveLikeCommand command = new(userIdWhoLike, bookId, userOpinionId);

            return await _mediatorHandler.SendCommandAsync(command);
        }
    }
}
