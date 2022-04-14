using AutoMapper;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Models.Filters;
using BookActivity.Domain.Commands.BookCommands;
using BookActivity.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation
{
    public class BookService : IBookService
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
            return await _mediatorHandler.SendCommand(_mapper.Map<AddBookCommand>(createBookModel));
        }

        public async Task<IList<BookDTO>> GetByFilterAsync(BookFilterModel filterModel)
        {
            var books = await _bookRepository.GetByAsync(filterModel.Condtion, filterModel.Skip, filterModel.Take);

            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO> GetByIdAsync(Guid bookId)
        {
            var book = await _bookRepository.GetByAsync(b => b.Id == bookId);

            return _mapper.Map<BookDTO>(book);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid bookId)
        {
            return await _mediatorHandler.SendCommand(new RemoveBookCommand(bookId));
        }

        public async Task<ValidationResult> UpdateActiveBookAsync(UpdateBookDTO updateBookCommand)
        {
            return await _mediatorHandler.SendCommand(_mapper.Map<UpdateBookCommand>(updateBookCommand));
        }
    }
}