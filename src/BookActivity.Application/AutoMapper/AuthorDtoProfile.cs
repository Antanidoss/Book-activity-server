using AutoMapper;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Domain.Commands.AuthorCommands.AddAuthor;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class AuthorDtoProfile : Profile
    {
        public AuthorDtoProfile()
        {
            CreateMap<CreateAuthorDto, AddAuthorCommand>();
        }
    }
}
