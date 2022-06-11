using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Filters;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Filters;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.Specifications.ActiveBookSpecs;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
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

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateActiveBookDTO updateActiveBookModel)
        {
            var updateActiveBookCommand = _mapper.Map<UpdateActiveBookCommand>(updateActiveBookModel);

            return await _mediatorHandler.SendCommand(updateActiveBookCommand);
        }

        public async Task<Result<IEnumerable<ActiveBookDTO>>> GetByFilterAsync(ActiveBookDTOFilterModel filterModel)
        {
            if (filterModel == null)
                return Result<IEnumerable<ActiveBookDTO>>.Invalid(new List<ValidationError> { new ValidationError() { ErrorMessage = ValidationErrorConstants.FilterModelIsNull } });

            ActiveBookFilterModel activeBookFilterModel = new(
                skip: filterModel.Skip == null ? BaseFilterModel.SkipDefault : filterModel.Skip.Value,
                take: filterModel.Take == null ? BaseFilterModel.TakeDefault : filterModel.Take.Value,
                filter: BuildFilter(filterModel));

            var activeBooks = await _activeBookRepository.GetByFilterAsync(activeBookFilterModel);

            return _mapper.Map<List<ActiveBookDTO>>(activeBooks);
        }

        private IQueryableFilterSpec<ActiveBook> BuildFilter(ActiveBookDTOFilterModel filterModel)
        {
            return new OrIQueryableFilterSpec<ActiveBook>(
                filterModel.ActiveBookIds == null ? null : new ActiveBookByIdSpec(filterModel.ActiveBookIds),
                filterModel.UserId == Guid.Empty ? null : new ActiveBookByUserIdSpec(filterModel.UserId));
        }
    }
}
