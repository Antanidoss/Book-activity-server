using AutoMapper;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Domain.Commands.AppUserCommands;
using BookActivity.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    public class AppUserService : IAppUserService
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

        public async Task<ValidationResult> AddAsync(AppUserCreateDTO appUserCreateDTO)
        {
            var addAppUserCommand = _mapper.Map<AddAppUserCommand>(appUserCreateDTO);

            return await _mediatorHandler.SendCommand(addAppUserCommand);
        }

        public async Task<AppUserDTO> FindByIdAsync(Guid appUserId)
        {
            var appUser = await _appUserRepository.FindByIdAsync(appUserId);

            return _mapper.Map<AppUserDTO>(appUser);
        }
    }
}
