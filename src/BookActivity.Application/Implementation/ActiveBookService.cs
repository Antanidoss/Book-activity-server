using AutoMapper;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Filters.FilterFacades;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation
{
    public sealed class ActiveBookService : IActiveBookService
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        private readonly IActiveBookRepository _activeBookRepository;

        public ActiveBookService(IMediatorHandler mediatorHandler, IActiveBookRepository activeBookRepository, IMapper mapper)
        {
            _mediatorHandler = mediatorHandler;
            _activeBookRepository = activeBookRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult> AddActiveBookAsync(CreateActiveBookDTO createEntity)
        {
            var addActiveBookCommand = _mapper.Map<AddActiveBookCommand>(createEntity);

            return await _mediatorHandler.SendCommand(addActiveBookCommand);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid entityId)
        {
            RemoveActiveBookCommand removeActiveBookCommand = new(entityId);

            return await _mediatorHandler.SendCommand(removeActiveBookCommand);
        }

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateActiveBookDTO entity)
        {
            var updateActiveBookCommand = _mapper.Map<UpdateActiveBookCommand>(entity);

            return await _mediatorHandler.SendCommand(updateActiveBookCommand);
        }

        public async Task<IList<ActiveBookDTO>> GetByFilterAsync(ActiveBookFilterModel filterModel)
        {
            var activeBooks = await _activeBookRepository.GetByFilterAsync(new ActiveBookFilter(filterModel));

            return _mapper.Map<List<ActiveBookDTO>>(activeBooks);
        }
    }
}
