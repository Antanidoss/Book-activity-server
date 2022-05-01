using AutoMapper;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.BookCommands;
using BookActivity.Domain.Filters.FilterFacades;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation
{
    public sealed class BookService : IBookService
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

        public async Task<ValidationResult> AddActiveBookAsync(CreateBookDTO createBookEntity)
        {
            var addBookCommand = _mapper.Map<AddBookCommand>(createBookEntity);

            return await _mediatorHandler.SendCommand(addBookCommand);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid bookId)
        {
            RemoveBookCommand removeBookCommand = new(bookId);

            return await _mediatorHandler.SendCommand(removeBookCommand);
        }

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateBookDTO updateBookEntity)
        {
            var updateBookCommand = _mapper.Map<UpdateBookCommand>(updateBookEntity);

            return await _mediatorHandler.SendCommand(updateBookCommand);
        }

        public async Task<IList<BookDTO>> GetByFilterAsync(BookFilterModel filterModel)
        {
            var books = await _bookRepository.GetByFilterAsync(new BookFilter(filterModel));

            return _mapper.Map<List<BookDTO>>(books);
        }
    }
}