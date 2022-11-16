using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.BookRatingCommands.UpdateBookRating;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class BookRatingService : IBookRatingService
    {
        private readonly IMediatorHandler _mediatorHandler;

        private readonly IMapper _mapper;

        public BookRatingService(IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> UpdateBookRatingAsync(UpdateBookRatingDto updateBookRating)
        {
            if (updateBookRating == null)
                return new ValidationResult(new List<ValidationFailure> { new(nameof(updateBookRating), "The update model cannot be null") });

            UpdateBookRatingCommand updateBookRatingCommand = new()
            {
                Id = updateBookRating.BookRatingId,
                BookOpinion = _mapper.Map<BookOpinion>(updateBookRating.BookOpinion)
            };

            return await _mediatorHandler.SendCommand(updateBookRatingCommand);
        }
    }
}
