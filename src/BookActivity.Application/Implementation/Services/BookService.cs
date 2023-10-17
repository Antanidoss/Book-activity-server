using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using BookActivity.Domain.Commands.BookCommands.RemoveBook;
using BookActivity.Domain.Commands.BookCommands.UpdateBook;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Validations;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class BookService : IBookService
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        public BookService(IMapper mapper, IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<ValidationResult> AddActiveBookAsync(CreateBookDto createBookModel)
        {
            createBookModel.Validate();

            var addBookCommand = _mapper.Map<AddBookCommand>(createBookModel);

            return await _mediatorHandler.SendCommandAsync(addBookCommand);
        }

        public async Task<ValidationResult> RemoveActiveBookAsync(Guid bookId, Guid userId)
        {
            CommonValidator.ThrowExceptionIfEmpty(bookId, nameof(bookId));

            RemoveBookCommand removeBookCommand = new(bookId, userId);

            return await _mediatorHandler.SendCommandAsync(removeBookCommand);
        }

        public async Task<ValidationResult> UpdateBookAsync(UpdateBookDto updateBookModel)
        {
            var updateBookCommand = _mapper.Map<UpdateBookCommand>(updateBookModel);

            return await _mediatorHandler.SendCommandAsync(updateBookCommand);
        }
    }
}