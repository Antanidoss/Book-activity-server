using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Filters;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.BookCommands;
using BookActivity.Domain.Extensions;
using BookActivity.Domain.Filters;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.Specifications.BookSpecs;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
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

        public async Task<Result<IEnumerable<BookDTO>>> GetByFilterAsync(BookDTOFilterModel filterModel)
        {
            if (filterModel == null)
                return Result<IEnumerable<BookDTO>>.Invalid(new List<ValidationError> { new ValidationError() { ErrorMessage = ValidationErrorConstants.FilterModelIsNull } });

            BookFilterModel bookFilter = new(
                skip: filterModel.Skip == null ? BaseFilterModel.SkipDefault : filterModel.Skip.Value,
                take: filterModel.Take == null ? BaseFilterModel.TakeDefault : filterModel.Take.Value,
                filter: BuildFilter(filterModel));

            var books = await _bookRepository.GetByFilterAsync(bookFilter);

            return _mapper.Map<List<BookDTO>>(books);
        }

        public IQueryableFilterSpec<Book> BuildFilter(BookDTOFilterModel filterModel)
        {
            return new BookByBookIdSpec(filterModel.BookIds).And(new BookByTitleContainsSpec(filterModel.Title));
        }
    }
}