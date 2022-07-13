using Antanidoss.Specification.Filters.Implementation;
using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Vidations;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.ActiveBookSpecs;
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

        public async Task<ValidationResult> AddActiveBookAsync(CreateActiveBookDTO createActiveBookModel)
        {
            var addActiveBookCommand = _mapper.Map<AddActiveBookCommand>(createActiveBookModel);

            return await _mediatorHandler.SendCommand(addActiveBookCommand);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid activeBookId)
        {
            RemoveActiveBookCommand removeActiveBookCommand = new(activeBookId);

            return await _mediatorHandler.SendCommand(removeActiveBookCommand);
        }

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateNumberPagesReadDTO updateActiveBookModel)
        {
            var updateActiveBookCommand = _mapper.Map<UpdateActiveBookCommand>(updateActiveBookModel);

            return await _mediatorHandler.SendCommand(updateActiveBookCommand);
        }

        public async Task<Result<IEnumerable<ActiveBookDTO>>> GetByActiveBookIdFilterAsync(PaginationModel paginationModel, Guid[] activeBookIds)
        {
            CommonValidator.ThrowExceptionIfNull(paginationModel);
            CommonValidator.ThrowExceptionIfNullOrEmpty(activeBookIds, nameof(activeBookIds));

            ActiveBookByIdSpec specification = new(activeBookIds);
            Where<ActiveBook> filter = new(specification);
            ActiveBookFilterModel activeBookFilterModel = new(filter, paginationModel.Skip, paginationModel.Take);
            var activeBooks = await _activeBookRepository.GetByFilterAsync(activeBookFilterModel);

            return _mapper.Map<List<ActiveBookDTO>>(activeBooks);
        }

        public async Task<Result<IEnumerable<ActiveBookDTO>>> GetByUserIdFilterAsync(PaginationModel paginationModel, Guid userId)
        {
            CommonValidator.ThrowExceptionIfNull(paginationModel);
            CommonValidator.ThrowExceptionIfEmpty(userId, nameof(userId));

            ActiveBookByUserIdSpec specification = new(userId);
            Where<ActiveBook> filter = new(specification);
            ActiveBookFilterModel activeBookFilterModel = new(filter, paginationModel.Skip, paginationModel.Take);
            var activeBooks = await _activeBookRepository.GetByFilterAsync(activeBookFilterModel);

            return _mapper.Map<List<ActiveBookDTO>>(activeBooks);
        }

        public async Task<Result<IEnumerable<ActiveBookHistoryData>>> GetActiveBookHistoryDataAsync(Guid activeBookId)
        {
            List<ActiveBookHistoryData> activeBookHistoryDateList = new();
            var storedEvents = await _eventStoreRepository.GetAllAsync(activeBookId);

            foreach (var storedEvent in storedEvents)
            {
                ActiveBookHistoryData activeBookHistoryData = JsonSerializer.Deserialize<ActiveBookHistoryData>(storedEvent.Data);

                switch (storedEvent.MessageType)
                {
                    case nameof(AddActiveBookEvent):
                        activeBookHistoryData.Action = "Registered";
                        break;
                    case nameof(UpdateActiveBookEvent):
                        activeBookHistoryData.Action = "Update";
                        break;
                    case nameof(RemoveActiveBookEvent):
                        activeBookHistoryData.Action = "Remove";
                        break;
                }

                activeBookHistoryData.UserId = storedEvent.User;
                activeBookHistoryDateList.Add(activeBookHistoryData);
            }

            return activeBookHistoryDateList;
        }
    }
}
