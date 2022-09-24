using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Domain.Commands.AuthorCommands.AddAuthor;
using NetDevPack.Mediator;
using System;
using System.Linq;
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

        public async Task<Result<Guid>> AddAsync(CreateAuthorDto createAuthor)
        {
            var addAuthorCommand = _mapper.Map<AddAuthorCommand>(createAuthor);

            var validationResult = await _mediatorHandler.SendCommand(addAuthorCommand);

            return validationResult.IsValid
                ? new Result<Guid>(addAuthorCommand.Id)
                : Result<Guid>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
        }
    }
}
