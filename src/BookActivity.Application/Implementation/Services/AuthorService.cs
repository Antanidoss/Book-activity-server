using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Domain.Commands.AuthorCommands.AddAuthor;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        public AuthorService(IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> AddAsync(CreateAuthorDto createAuthor)
        {
            var addAuthorCommand = _mapper.Map<AddAuthorCommand>(createAuthor);

            return await _mediatorHandler.SendCommand(addAuthorCommand);
        }
    }
}
