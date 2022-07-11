using Antanidoss.Specification.Filters.Implementation;
using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Vidations;
using BookActivity.Domain.Commands.BookCommands;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class BookService : IBookService
    {
        private readonly IMapper _mapper;

        private readonly IBookRepository _bookRepository;

        private readonly IMediatorHandler _mediatorHandler;

        public BookService(IMapper mapper, IBookRepository bookRepository, IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<ValidationResult> AddActiveBookAsync(CreateBookDTO createBookModel)
        {
            var addBookCommand = _mapper.Map<AddBookCommand>(createBookModel);

            return await _mediatorHandler.SendCommand(addBookCommand);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid bookId)
        {
            CommonValidator.ThrowExceptionIfEmpty(bookId, nameof(bookId));

            RemoveBookCommand removeBookCommand = new(bookId);

            return await _mediatorHandler.SendCommand(removeBookCommand);
        }

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateBookDTO updateBookModel)
        {
            var updateBookCommand = _mapper.Map<UpdateBookCommand>(updateBookModel);

            return await _mediatorHandler.SendCommand(updateBookCommand);
        }

        public async Task<Result<IEnumerable<BookDTO>>> GetByBookIdsFilterAsync(PaginationModel paginationModel, Guid[] bookIds)
        {
            CommonValidator.ThrowExceptionIfNull(paginationModel);
            CommonValidator.ThrowExceptionIfNullOrEmpty(bookIds, nameof(bookIds));

            BookByIdSpec specification = new(bookIds);
            Where<Book> filter = new(specification);
            BookFilterModel bookFilter = new(filter, paginationModel.Skip, paginationModel.Take);
            var books = await _bookRepository.GetByFilterAsync(bookFilter);

            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<Result<IEnumerable<BookDTO>>> GetByTitleContainsFilterAsync(PaginationModel paginationModel, string title)
        {
            CommonValidator.ThrowExceptionIfNull(paginationModel);

            BookByTitleContainsSpec specification = new(title);
            Where<Book> filter = new(specification);
            BookFilterModel bookFilter = new(filter, paginationModel.Skip, paginationModel.Take);
            var books = await _bookRepository.GetByFilterAsync(bookFilter);

            return _mapper.Map<List<BookDTO>>(books);
        }
    }
}