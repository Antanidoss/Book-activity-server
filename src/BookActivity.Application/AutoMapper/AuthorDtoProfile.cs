using AutoMapper;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Domain.Commands.AuthorCommands.AddAuthor;
using BookActivity.Domain.Models;

namespace BookActivity.Application.AutoMapper
{
    internal sealed class AuthorDtoProfile : Profile
    {
        public AuthorDtoProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, AddAuthorCommand>();
        }
    }
}
