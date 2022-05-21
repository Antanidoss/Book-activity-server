using AutoMapper;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Domain.Commands.AppUserCommands;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    public class AppUserDTOProfile : Profile
    {
        public AppUserDTOProfile()
        {
            CreateMap<AppUser, AppUserDTO>();
            CreateMap<AppUserDTO, AppUser>();
            CreateMap<AppUserCreateDTO, AddAppUserCommand>();
        }
    }
}
