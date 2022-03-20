using AutoMapper;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Models.Filters;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation
{
    public class ActiveBookService : IActiveBookService
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

        public async Task<ValidationResult> AddActiveBookAsync(CreateActiveBookDTO entity)
        {
            return await _mediatorHandler.SendCommand(_mapper.Map<AddActiveBookCommand>(entity));
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid entityId)
        {
            return await _mediatorHandler.SendCommand(new RemoveActiveBookCommand(entityId));
        }

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateActiveBookDTO entity)
        {
            return await _mediatorHandler.SendCommand(_mapper.Map<UpdateActiveBookCommand>(entity));
        }

        public async Task<IList<ActiveBookDTO>> GetByFilterAsync(ActiveBookFilterModel filterModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ActiveBookDTO> GetByIdAsync(Guid entityId)
        {
            return _mapper.Map<ActiveBookDTO>(await _activeBookRepository.GetByAsync(a => a.Id == entityId));
        }
    }
}
