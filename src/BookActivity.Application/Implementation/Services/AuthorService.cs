using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Domain.Commands.AuthorCommands.AddAuthor;
using System;
using System.Threading.Tasks;
using BookActivity.Domain.Interfaces;

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
            createAuthor.Validate();

            var addAuthorCommand = _mapper.Map<AddAuthorCommand>(createAuthor);

            var validationResult = await _mediatorHandler.SendCommandAsync(addAuthorCommand);

            return validationResult.ToResult(addAuthorCommand.Id);
        }
    }
}
