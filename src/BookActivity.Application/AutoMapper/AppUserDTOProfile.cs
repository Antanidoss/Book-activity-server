using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.AppUserCommands.AddAppUser;
using BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser;
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
            CreateMap<UpdateAppUserDTO, UpdateAppUserCommand>()
                .ForMember(u => u.AvatarImage, conf => conf.MapFrom(u => u.AvatarImage.ConvertToBuffer()))
                .ForMember(u => u.AppUserId, conf => conf.MapFrom(u => u.AppUserId))
                .ForMember(u => u.Name, conf => conf.MapFrom(u => u.UserName))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
