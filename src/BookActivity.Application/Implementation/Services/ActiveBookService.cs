using Antanidoss.Specification.Filters.Implementation;
using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.ActiveBookSpecs;
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

        public async Task<Result<IEnumerable<ActiveBookDTO>>> GetByActiveBookIdFilterAsync(PaginationModel paginationModel, Guid[] activeBookIds)
        {
            if (paginationModel == null)
                return Result<IEnumerable<ActiveBookDTO>>.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = ValidationErrorConstants.FilterModelIsNull } });

            ActiveBookByIdSpec specification = new(activeBookIds);
            Where<ActiveBook> filter = new(specification);
            ActiveBookFilterModel activeBookFilterModel = new(filter, paginationModel.Skip, paginationModel.Take);
            var activeBooks = await _activeBookRepository.GetByFilterAsync(activeBookFilterModel);

            return _mapper.Map<List<ActiveBookDTO>>(activeBooks);
        }

        public async Task<Result<IEnumerable<ActiveBookDTO>>> GetByUserIdFilterAsync(PaginationModel filterModel, Guid userId)
        {
            if(filterModel == null)
                return Result<IEnumerable<ActiveBookDTO>>.Invalid(new List<ValidationError> { new ValidationError { ErrorMessage = ValidationErrorConstants.FilterModelIsNull } });

            ActiveBookByUserIdSpec specification = new(userId);
            Where<ActiveBook> filter = new(specification);
            ActiveBookFilterModel activeBookFilterModel = new(filter, filterModel.Skip, filterModel.Take);
            var activeBooks = await _activeBookRepository.GetByFilterAsync(activeBookFilterModel);

            return _mapper.Map<List<ActiveBookDTO>>(activeBooks);
        }
    }
}
