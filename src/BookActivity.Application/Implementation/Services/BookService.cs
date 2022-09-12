using Antanidoss.Specification.Filters.Implementation;
using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using BookActivity.Domain.Commands.BookCommands.RemoveBook;
using BookActivity.Domain.Commands.BookCommands.UpdateBook;
using BookActivity.Domain.Events.BookEvents;
using BookActivity.Domain.FilterModels;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using BookActivity.Domain.Vidations;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
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

        public async Task<ValidationResult> AddActiveBookAsync(CreateBookDTO createBookModel)
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

        public async Task<ValidationResult> UpdateBookAsync(UpdateBookDTO updateBookModel)
        {
            var updateBookCommand = _mapper.Map<UpdateBookCommand>(updateBookModel);

            return await _mediatorHandler.SendCommand(updateBookCommand).ConfigureAwait(false);
        }

        public async Task<Result<IEnumerable<BookDTO>>> GetByBookIdsAsync(Guid[] bookIds)
        {
            CommonValidator.ThrowExceptionIfNullOrEmpty(bookIds, nameof(bookIds));

            BookByIdSpec specification = new(bookIds);
            Where<Book> filter = new(specification);
            PaginationModel paginationModel = new(take: bookIds.Length); 
            BookFilterModel bookFilter = new(filter, paginationModel.Skip, paginationModel.Take);

            var books = await _bookRepository.GetByFilterAsync(bookFilter).ConfigureAwait(false);

            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<Result<IEnumerable<BookDTO>>> GetByTitleContainsAsync(PaginationModel paginationModel, string title)
        {
            CommonValidator.ThrowExceptionIfNull(paginationModel);

            BookByTitleContainsSpec specification = new(title);
            Where<Book> filter = new(specification);
            BookFilterModel bookFilter = new(filter, paginationModel.Skip, paginationModel.Take);

            var books = await _bookRepository.GetByFilterAsync(bookFilter).ConfigureAwait(false);

            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<Result<IEnumerable<BookDTO>>> GetByPaginationAsync(PaginationModel paginationModel, Guid currentUserId)
        {
            CommonValidator.ThrowExceptionIfNull(paginationModel);

            BookFilterModel filterModel = new(paginationModel.Skip, paginationModel.Take);
            var books = (await _bookRepository.GetByFilterAsync(filterModel, b => b.ActiveBooks)).ToArray();
            var booksDto = _mapper.Map<IEnumerable<BookDTO>>(books).ToArray();

            if (currentUserId != Guid.Empty)
                for (int i = 0; i < books.Count(); i++)
                    if (books[i].ActiveBooks.Any(a => a.UserId == currentUserId))
                        booksDto[i].IsActiveBook = true;

            return new Result<IEnumerable<BookDTO>>(booksDto);
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