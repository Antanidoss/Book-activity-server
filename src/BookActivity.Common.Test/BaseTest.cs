using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BookActivity.Common.Test
{
    public class BaseTest
    {
        public delegate Task TestAction(IServiceProvider serviceProvider, IDbContext dbContext);

        public IConfiguration Configuration { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        [SetUp]
        public virtual async Task SetUp()
        {
            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.AddJsonFile("appsettings.json");
            Configuration = configurationBuilder.Build();

            ServiceCollection services = new();
            ServiceProvider = await ConfigureServicesAsync(services);

            var dbContext = ServiceProvider.GetRequiredService<IDbContext>();
            dbContext.ClearChangeTracker();
        }

        public virtual async Task<IServiceProvider> ConfigureServicesAsync(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton(Configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            Domain.ModuleConfiguration domainModuleConfigure = new();
            domainModuleConfigure.ConfigureDI(services, Configuration);

            Infrastructure.ModuleConfiguration infrastructureModuleConfigure = new();
            infrastructureModuleConfigure.ConfigureDI(services, Configuration);

            Infrastructure.Data.ModuleConfiguration infrastructureDataModuleConfigure = new();
            infrastructureDataModuleConfigure.ConfigureDI(services, Configuration);

            Application.ModuleConfiguration applicationModuleConfigure = new();
            applicationModuleConfigure.ConfigureDI(services, Configuration);

            services.AddScoped(_ => DbConstants.CurrentUser);

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            infrastructureDataModuleConfigure.CreateDatabasesIfNotExist(scope);

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            await userManager.CreateAsync(new AppUser { Id = DbConstants.CurrentUser.Id, UserName = DbConstants.CurrentUser.UserName, Email = DbConstants.CurrentUserEmail });
            return serviceProvider;
        }

        protected async Task BeginTransactionAsync(TestAction actionAsync)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var localServiceProvider = scope.ServiceProvider;
                var dataContext = localServiceProvider.GetRequiredService<IDbContext>();
                var connection = dataContext.Database.GetDbConnection();
                if (connection?.State == System.Data.ConnectionState.Open)
                    await connection.CloseAsync();

                var strategy = dataContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    async Task runBodyAsync() => await actionAsync(localServiceProvider, dataContext);

                    await using var tran = await dataContext.Database.BeginTransactionAsync();

                    try
                    {
                        await runBodyAsync();
                    }
                    finally
                    {
                        await tran.RollbackAsync();
                    }
                });

                await connection.CloseAsync();
            }
        }
    }
}
