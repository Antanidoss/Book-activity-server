﻿using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.AppUserCommands.AddAppUser;
using BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class AppUserDtoProfile : Profile
    {
        public AppUserDtoProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserDto, AppUser>();
            CreateMap<CreateAppUserDto, AddAppUserCommand>()
                 .ForMember(u => u.AvatarImage, conf => conf.MapFrom(u => u.AvatarImage.ConvertToBuffer()));

            CreateMap<UpdateAppUserDto, UpdateAppUserCommand>()
                .ForMember(u => u.AvatarImage, conf => conf.MapFrom(u => u.AvatarImage.ConvertToBuffer()))
                .ForMember(u => u.AppUserId, conf => conf.MapFrom(u => u.AppUserId))
                .ForMember(u => u.Name, conf => conf.MapFrom(u => u.UserName))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
