using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.AppUserCommands.AddAppUser;
using BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.AppUserQueries.AuthenticationUser;
using BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class AppUserDtoProfile : Profile
    {
        public AppUserDtoProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserDto, AppUser>();
            CreateMap<AuthenticationModel, AuthenticationUserQuery>();
            CreateMap<GetUsersByFilterDto, GetUsersByFilterQuery>();
            CreateMap<CreateAppUserDto, AddAppUserCommand>()
                 .ForMember(u => u.AvatarImage, conf => conf.MapFrom(u => u.AvatarImage.ConvertToBuffer()));

            CreateMap<UpdateAppUserDto, UpdateAppUserCommand>()
                .ForMember(u => u.AvatarImage, conf => conf.MapFrom(u => u.AvatarImage.ConvertToBuffer()))
                .ForMember(u => u.AppUserId, conf => conf.MapFrom(u => u.UserId))
                .ForMember(u => u.Name, conf => conf.MapFrom(u => u.Name))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
