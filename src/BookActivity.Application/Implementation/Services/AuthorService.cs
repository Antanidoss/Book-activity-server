using Antanidoss.Specification.Filters.Implementation;
using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Extensions;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Domain.Commands.AuthorCommands.AddAuthor;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AuthorSpecs;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IMediatorHandler mediatorHandler, IMapper mapper, IAuthorRepository authorRepository)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<Result<Guid>> AddAsync(CreateAuthorDto createAuthor)
        {
            var addAuthorCommand = _mapper.Map<AddAuthorCommand>(createAuthor);

            var validationResult = await _mediatorHandler.SendCommand(addAuthorCommand);

            return validationResult.ToResult(addAuthorCommand.Id);
        }

        public async Task<Result<IEnumerable<AuthorDto>>> GetAllAuthorsAsync()
        {
            var authorCount = await _authorRepository.GetCountByFilterAsync();
            var authors = await _authorRepository.GetByFilterAsync(new AuthorFilterModel(take: authorCount)).ConfigureAwait(false);

            return new Result<IEnumerable<AuthorDto>>(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        public async Task<Result<IEnumerable<AuthorDto>>> GetAuthorsByNameAsync(string name, int take)
        {
            AuthorByNameSpec authorByNameSpec = new(name);
            Where<Author> filter = new(authorByNameSpec);
            AuthorFilterModel authorFilterModel = new(filter, take: take);

            var authors = await _authorRepository.GetByFilterAsync(authorFilterModel).ConfigureAwait(false);

            return new Result<IEnumerable<AuthorDto>>(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }
    }
}
