using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.AppUserCommands.AddAppUser;
using BookActivity.Domain.Commands.AppUserCommands.SubscribeAppUser;
using BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Queries.AppUserQueries.AuthenticationUser;
using BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter;
using BookActivity.Domain.Specifications.AppUserSpecs;
using BookActivity.Domain.Validations;
using BookActivity.Shared.Models;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal class AppUserService : IAppUserService
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        private readonly IAppUserRepository _appUserRepository;

        public AppUserService(IMapper mapper, IMediatorHandler mediatorHandler, IAppUserRepository appUserRepository)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _appUserRepository = appUserRepository;
        }

        public async Task<ValidationResult> AddAsync(CreateAppUserDto appUserCreateDTO)
        {
            var addAppUserCommand = _mapper.Map<AddAppUserCommand>(appUserCreateDTO);

            return await _mediatorHandler.SendCommand(addAppUserCommand);
        }

        public async Task<ValidationResult> SubscribeAppUserAsync(Guid currentUserId, Guid subscribedUserId)
        {
            CommonValidator.ThrowExceptionIfEmpty(currentUserId, nameof(currentUserId));
            CommonValidator.ThrowExceptionIfEmpty(subscribedUserId, nameof(subscribedUserId));

            SubscribeAppUserCommand subscribeAppUserCommand = new(currentUserId, subscribedUserId);

            return await _mediatorHandler.SendCommand(subscribeAppUserCommand).ConfigureAwait(false);
        }

        public async Task<ValidationResult> UpdateAsync(UpdateAppUserDto updateAppUserModel)
        {
            CommonValidator.ThrowExceptionIfEmpty(updateAppUserModel.UserId, nameof(updateAppUserModel.UserId));

            return await _mediatorHandler.SendCommand(_mapper.Map<UpdateAppUserCommand>(updateAppUserModel)).ConfigureAwait(false);
        }

        public async Task<Result<AuthenticationResult>> AuthenticationAsync(AuthenticationModel authenticationModel)
        {
            CommonValidator.ThrowExceptionIfNull(authenticationModel);

            var query = _mapper.Map<AuthenticationUserQuery>(authenticationModel);

            return await _mediatorHandler.SendQuery(query).ConfigureAwait(false);
        }

        public async Task<Result<AppUserDto>> FindByIdAsync(Guid appUserId)
        {
            CommonValidator.ThrowExceptionIfEmpty(appUserId, nameof(appUserId));

            AppUserByIdSpec specification = new(appUserId);
            var appUser = await _appUserRepository.GetBySpecAsync(specification).ConfigureAwait(false);

            return _mapper.Map<AppUserDto>(appUser);
        }

        public async Task<EntityListResult<SelectedAppUser>> GetAppUserByFilter(GetUsersByFilterQuery filterModel)
        {
            return await _mediatorHandler.SendQuery(filterModel);
        }
    }
}
