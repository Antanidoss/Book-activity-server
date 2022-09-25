using Antanidoss.Specification.Filters.Implementation;
using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook;
using BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.FilterModels;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.ActiveBookSpecs;
using BookActivity.Domain.Vidations;
using FluentValidation.Results;
using NetDevPack.Mediator;
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

        public async Task<Result<Guid>> AddActiveBookAsync(CreateActiveBookDto createActiveBookModel)
        {
            var addActiveBookCommand = _mapper.Map<AddActiveBookCommand>(createActiveBookModel);

            var validationResult = await _mediatorHandler.SendCommand(addActiveBookCommand).ConfigureAwait(false);

            return validationResult.ToResult(addActiveBookCommand.Id);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid activeBookId)
        {
            CommonValidator.ThrowExceptionIfEmpty(activeBookId, nameof(activeBookId));

            RemoveActiveBookCommand removeActiveBookCommand = new() { Id = activeBookId };

            return await _mediatorHandler.SendCommand(removeActiveBookCommand).ConfigureAwait(false);
        }

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateNumberPagesReadDto updateActiveBookModel)
        {
            var updateActiveBookCommand = _mapper.Map<UpdateActiveBookCommand>(updateActiveBookModel);

            return await _mediatorHandler.SendCommand(updateActiveBookCommand).ConfigureAwait(false);
        }

        public async Task<Result<IEnumerable<ActiveBookDto>>> GetByActiveBookIdAsync(Guid[] activeBookIds)
        {
            CommonValidator.ThrowExceptionIfNullOrEmpty(activeBookIds, nameof(activeBookIds));

            ActiveBookByIdSpec specification = new(activeBookIds);
            Where<ActiveBook> filter = new(specification);
            PaginationModel paginationModel = new(take: activeBookIds.Length);
            ActiveBookFilterModel activeBookFilterModel = new(filter, paginationModel.Skip, paginationModel.Take);

            var activeBooks = await _activeBookRepository.GetByFilterAsync(activeBookFilterModel).ConfigureAwait(false);

            return _mapper.Map<List<ActiveBookDto>>(activeBooks);
        }

        public async Task<Result<IEnumerable<ActiveBookDto>>> GetByUserIdAsync(PaginationModel paginationModel, Guid currentUserId)
        {
            CommonValidator.ThrowExceptionIfNull(paginationModel);

            ActiveBookByUserIdSpec specification = new(currentUserId);
            Where<ActiveBook> filter = new(specification);
            ActiveBookFilterModel activeBookFilterModel = new(filter, paginationModel.Skip, paginationModel.Take);
            var activeBooks = await _activeBookRepository.GetByFilterAsync(activeBookFilterModel, a => a.Book).ConfigureAwait(false);

            return _mapper.Map<List<ActiveBookDto>>(activeBooks);
        }

        public async Task<Result<IEnumerable<ActiveBookHistoryData>>> GetActiveBookHistoryDataAsync(Guid activeBookId)
        {
            CommonValidator.ThrowExceptionIfEmpty(activeBookId, nameof(activeBookId));

            List<ActiveBookHistoryData> activeBookHistoryDateList = new();
            var storedEvents = await _eventStoreRepository.GetAllAsync(activeBookId).ConfigureAwait(false);

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

                activeBookHistoryData.UserId = storedEvent.User;
                activeBookHistoryDateList.Add(activeBookHistoryData);
            }

            return activeBookHistoryDateList;
        }
    }
}
