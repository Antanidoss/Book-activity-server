using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.BookCommands;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.Specifications.BookSpecs;
using BookActivity.Domain.Interfaces.Repositories;
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
            if (paginationModel == null)
                return Result<IEnumerable<BookDTO>>.Invalid(new List<ValidationError> { new ValidationError() { ErrorMessage = ValidationErrorConstants.FilterModelIsNull } });

            BookFilterModel bookFilter = new(
                skip: paginationModel.Skip,
                take: paginationModel.Take,
                filter: new BookByBookIdSpec(bookIds));

            var books = await _bookRepository.GetByFilterAsync(bookFilter);

            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<Result<IEnumerable<BookDTO>>> GetByTitleContainsFilterAsync(PaginationModel paginationModel, string title)
        {
            if (paginationModel == null)
                return Result<IEnumerable<BookDTO>>.Invalid(new List<ValidationError> { new ValidationError() { ErrorMessage = ValidationErrorConstants.FilterModelIsNull } });

            BookFilterModel bookFilter = new(
                skip: paginationModel.Skip,
                take: paginationModel.Take,
                filter: new BookByTitleContainsSpec(title));

            var books = await _bookRepository.GetByFilterAsync(bookFilter);

            return _mapper.Map<List<BookDTO>>(books);
        }
    }
}