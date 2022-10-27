using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Implementation.Filters;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using BookActivity.Domain.Commands.BookCommands.RemoveBook;
using BookActivity.Domain.Commands.BookCommands.UpdateBook;
using BookActivity.Domain.Events.BookEvents;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using BookActivity.Domain.Validations;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class BookService : IBookService
    {
        private readonly IMapper _mapper;

        private readonly IBookRepository _bookRepository;

        private readonly IMediatorHandler _mediatorHandler;

        private readonly IEventStoreRepository _eventStoreRepository;

        public BookService(IMapper mapper, IBookRepository bookRepository, IMediatorHandler mediatorHandler, IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _mediatorHandler = mediatorHandler;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<ValidationResult> AddActiveBookAsync(CreateBookDto createBookModel)
        {
            var addBookCommand = _mapper.Map<AddBookCommand>(createBookModel);

            return await _mediatorHandler.SendCommand(addBookCommand);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid bookId)
        {
            CommonValidator.ThrowExceptionIfEmpty(bookId, nameof(bookId));

            RemoveBookCommand removeBookCommand = new(bookId);

            return await _mediatorHandler.SendCommand(removeBookCommand).ConfigureAwait(false);
        }

        public async Task<ValidationResult> UpdateBookAsync(UpdateBookDto updateBookModel)
        {
            var updateBookCommand = _mapper.Map<UpdateBookCommand>(updateBookModel);

            return await _mediatorHandler.SendCommand(updateBookCommand).ConfigureAwait(false);
        }

        public async Task<Result<IEnumerable<BookDto>>> GetByBookIdsAsync(Guid[] bookIds)
        {
            CommonValidator.ThrowExceptionIfNullOrEmpty(bookIds, nameof(bookIds));

            BookByIdSpec specification = new(bookIds);
            PaginationModel paginationModel = new(take: bookIds.Length); 

            var books = await _bookRepository.GetBySpecAsync(specification, paginationModel).ConfigureAwait(false);

            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<Result<IEnumerable<BookDto>>> GetByFilterAsync(BookFilterModel bookFilterModel)
        {
            if (bookFilterModel == null)
                throw new ArgumentNullException(nameof(bookFilterModel));

            Func<IQueryable<Book>, IQueryable<Book>> filter = (query) =>
            {
                return query
                    .ApplyBookFilter(bookFilterModel)
                    .ApplyPaginaton(bookFilterModel.Skip, bookFilterModel.Take);
            };

            var books = await _bookRepository.GetByFilterAsync(filter, b => b.BookRating, b => b.BookRating.BookOpinions).ConfigureAwait(false);

            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<Result<IEnumerable<BookHistoryData>>> GetBookHistoryDataAsync(Guid bookId)
        {
            CommonValidator.ThrowExceptionIfEmpty(bookId, nameof(bookId));

            List<BookHistoryData> bookHistoryDateList = new();
            var storedEvents = await _eventStoreRepository.GetAllAsync(bookId).ConfigureAwait(false);

            foreach (var storedEvent in storedEvents)
            {
                var bookHistoryData = JsonSerializer.Deserialize<BookHistoryData>(storedEvent.Data);

                switch (storedEvent.MessageType)
                {
                    case nameof(AddBookEvent):
                        bookHistoryData.Action = ActionNamesConstants.Registered;
                        break;
                    case nameof(UpdateBookEvent):
                        bookHistoryData.Action = ActionNamesConstants.Update;
                        break;
                    case nameof(RemoveBookEvent):
                        bookHistoryData.Action = ActionNamesConstants.Remove;
                        break;
                }

                bookHistoryData.UserId = storedEvent.User;
                bookHistoryDateList.Add(bookHistoryData);
            }

            return bookHistoryDateList;
        }
    }
}