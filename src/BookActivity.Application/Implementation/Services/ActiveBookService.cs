using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook;
using BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class ActiveBookService : IActiveBookService
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        private readonly IActiveBookRepository _activeBookRepository;

        private readonly IEventStoreRepository _eventStoreRepository;

        public ActiveBookService(IMediatorHandler mediatorHandler, IActiveBookRepository activeBookRepository, IMapper mapper, IEventStoreRepository eventStoreRepository)
        {
            _mediatorHandler = mediatorHandler;
            _activeBookRepository = activeBookRepository;
            _mapper = mapper;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<Result<IEnumerable<ActiveBookHistoryData>>> GetActiveBookHistoryDataAsync(Guid activeBookId)
        {
            CommonValidator.ThrowExceptionIfEmpty(activeBookId, nameof(activeBookId));

            List<ActiveBookHistoryData> activeBookHistoryDateList = new();
            var storedEvents = await _eventStoreRepository.GetAllAsync(activeBookId);

            foreach (var storedEvent in storedEvents)
            {
                var activeBookHistoryData = JsonSerializer.Deserialize<ActiveBookHistoryData>(storedEvent.Data);

                switch (storedEvent.MessageType)
                {
                    case nameof(AddActiveBookEvent):
                        activeBookHistoryData.Action = ActionNamesConstants.Registered;
                        break;
                    case nameof(UpdateActiveBookEvent):
                        activeBookHistoryData.Action = ActionNamesConstants.Update;
                        break;
                    case nameof(RemoveActiveBookEvent):
                        activeBookHistoryData.Action = ActionNamesConstants.Remove;
                        break;
                }

                activeBookHistoryData.UserId = storedEvent.UserId.ToString();
                activeBookHistoryDateList.Add(activeBookHistoryData);
            }

            return activeBookHistoryDateList;
        }

        public async Task<Result<Guid>> AddActiveBookAsync(CreateActiveBookDto createActiveBookModel)
        {
            createActiveBookModel.Validate();

            var addActiveBookCommand = _mapper.Map<AddActiveBookCommand>(createActiveBookModel);

            var validationResult = await _mediatorHandler.SendCommandAsync(addActiveBookCommand);

            return validationResult.ToResult(addActiveBookCommand.Id);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid activeBookId)
        {
            CommonValidator.ThrowExceptionIfEmpty(activeBookId, nameof(activeBookId));

            RemoveActiveBookCommand removeActiveBookCommand = new() { Id = activeBookId };

            return await _mediatorHandler.SendCommandAsync(removeActiveBookCommand);
        }

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateNumberPagesReadDto updateActiveBookModel)
        {
            var updateActiveBookCommand = _mapper.Map<UpdateActiveBookCommand>(updateActiveBookModel);

            return await _mediatorHandler.SendCommandAsync(updateActiveBookCommand);
        }
    }
}
