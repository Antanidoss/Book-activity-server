using AutoMapper;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.AppUserCommands;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class AppUserDTOProfile : Profile
    {
        public AppUserDTOProfile()
        {
            CreateMap<AppUser, AppUserDTO>();
            CreateMap<AppUserDTO, AppUser>();
            CreateMap<AppUserCreateDTO, AddAppUserCommand>();
            CreateMap<UpdateAppUserDTO, UpdateAppUserCommand>();
        }
    }
}
