using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AuthorCommands.AddAuthor
{
    internal sealed class AddAuthorCommandHandler : CommandHandler,
        IRequestHandler<AddAuthorCommand, ValidationResult>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ValidationResult> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            _authorRepository.Add(new Author ( request.FirstName, request.Surname, request.Patronymic));

            return await Commit(_authorRepository.UnitOfWork);
        }
    }
}
