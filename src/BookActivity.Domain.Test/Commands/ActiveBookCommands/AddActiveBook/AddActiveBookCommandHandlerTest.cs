using BookActivity.Common.Test;
using BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook;
using BookActivity.Domain.Interfaces.Hubs;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace BookActivity.Domain.Test.Commands.ActiveBookCommands.AddActiveBook
{
    public class AddActiveBookCommandHandlerTest : BaseTest
    {
        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            Mock<INotificationsHub> notificationsHubMock = new();
            services.Replace(new ServiceDescriptor(typeof(INotificationsHub), notificationsHubMock.Object));

            return services.BuildServiceProvider();
        }

        [Test]
        public async Task AddActiveBook_Ok_Async()
        {
            await BeginTransactionAsync(async (serviceProvider, dbContext) =>
            {
                var book = await DbDataCreator.CreateBookAsync(dbContext);

                var addActiveBookCommandHandler = serviceProvider.GetRequiredService<IRequestHandler<AddActiveBookCommand, ValidationResult>>();

                AddActiveBookCommand addActiveBookCommand = new() { BookId = book.Id, NumberPagesRead = 0, TotalNumberPages = 580, UserId = _currentUser.Id };
                await addActiveBookCommandHandler.Handle(addActiveBookCommand, cancellationToken: default);
            });
        }
    }
}
