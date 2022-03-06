using BookActivity.Domain.Commands.BookActiveCommands;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Infrastructure.Data.Repositories;
using BookActivity.Infrastructure.Data.Repositories.EventSourcing;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;

namespace BookActivity.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBaseInfastructure(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped<IActiveBookRepository, ActiveBookRepository>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

            services.AddScoped<IRequestHandler<AddNewBookActiveCommand, ValidationResult>, ActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();

            return services;
        }
    }
}