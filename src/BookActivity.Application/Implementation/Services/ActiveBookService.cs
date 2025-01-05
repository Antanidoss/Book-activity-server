using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook;
using BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class ActiveBookService : IActiveBookService
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        public ActiveBookService(IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> AddActiveBookAsync(CreateActiveBookDto createActiveBookModel)
        {
            createActiveBookModel.Validate();

            var addActiveBookCommand = _mapper.Map<AddActiveBookCommand>(createActiveBookModel);

            var validationResult = await _mediatorHandler.SendCommandAsync(addActiveBookCommand);

            return validationResult.ToResult(addActiveBookCommand.Id);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid activeBookId, Guid currentUserId)
        {
            CommonValidator.ThrowExceptionIfEmpty(activeBookId, nameof(activeBookId));

            RemoveActiveBookCommand removeActiveBookCommand = new(activeBookId, currentUserId);

            return await _mediatorHandler.SendCommandAsync(removeActiveBookCommand);
        }

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateNumberPagesReadDto updateActiveBookModel)
        {
            var updateActiveBookCommand = _mapper.Map<UpdateActiveBookCommand>(updateActiveBookModel);

            return await _mediatorHandler.SendCommandAsync(updateActiveBookCommand);
        }
    }
}
