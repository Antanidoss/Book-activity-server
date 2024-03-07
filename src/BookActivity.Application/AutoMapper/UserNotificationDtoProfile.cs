using AutoMapper;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class UserNotificationDtoProfile : Profile
    {
        public UserNotificationDtoProfile()
        {
            CreateMap<Notification, UserNotificationDto>();
        }
    }
}
