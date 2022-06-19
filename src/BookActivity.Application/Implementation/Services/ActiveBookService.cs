using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.Specifications.ActiveBookSpecs;
using BookActivity.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class ActiveBookService : IActiveBookService
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

        public async Task<Result<IEnumerable<ActiveBookDTO>>> GetByActiveBookIdFilterAsync(PaginationModel filterModel, Guid[] activeBookIds)
        {
            if (filterModel == null)
                return Result<IEnumerable<ActiveBookDTO>>.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = ValidationErrorConstants.FilterModelIsNull } });

            ActiveBookFilterModel activeBookFilterModel = new(
                skip: filterModel.Skip,
                take: filterModel.Take,
                filter: new ActiveBookByIdSpec(activeBookIds));

            var activeBooks = await _activeBookRepository.GetByFilterAsync(activeBookFilterModel);

            return _mapper.Map<List<ActiveBookDTO>>(activeBooks);
        }

        public async Task<Result<IEnumerable<ActiveBookDTO>>> GetByUserIdFilterAsync(PaginationModel filterModel, Guid userId)
        {
            if(filterModel == null)
                return Result<IEnumerable<ActiveBookDTO>>.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = ValidationErrorConstants.FilterModelIsNull } });

            ActiveBookFilterModel activeBookFilterModel = new(
                skip: filterModel.Skip,
                take: filterModel.Take,
                filter: new ActiveBookByUserIdSpec(userId));

            var activeBooks = await _activeBookRepository.GetByFilterAsync(activeBookFilterModel);

            return _mapper.Map<List<ActiveBookDTO>>(activeBooks);
        }
    }
}
