using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BookActivity.Shared.Models;

namespace BookActivity.Common.Test
{
    public class BaseTest
    {
        public delegate Task TestAction(IServiceProvider serviceProvider, IDbContext dbContext);
        public IConfiguration Configuration { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }
        protected static bool _initDbData; 
        protected static CurrentUser _currentUser { get; private set; }

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.AddJsonFile("appsettings.json");
            Configuration = configurationBuilder.Build();

            ServiceCollection services = new();
            ServiceProvider = ConfigureServices(services);

            if (!_initDbData)
                await InitDbDataAsync();
        }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
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

            services.AddScoped(_ => _currentUser);

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            infrastructureDataModuleConfigure.CreateDatabasesIfNotExist(scope);

            return serviceProvider;
        }

        protected async Task BeginTransactionAsync(TestAction actionAsync, bool withRollback = true)
        {
            using var scope = ServiceProvider.CreateScope();
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
                    if (withRollback)
                        await tran.RollbackAsync();
                }
            });

            await connection.CloseAsync();
        }

        private async Task InitDbDataAsync()
        {
            if (_initDbData)
                return;

            await BeginTransactionAsync(async (serviceProvider, dbContext) =>
            {
                var currentUser = await dbContext.Users.FirstOrDefaultAsync();
                if (currentUser != null)
                {
                    _initDbData = true;
                    return;
                }

                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                await userManager.CreateAsync(new AppUser
                {
                    UserName = "Anton",
                    Email = DbConstants.CurrentUserEmail
                });
            }, withRollback: false);

            using var scope = ServiceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

            var currentUser = await dbContext.Users.FirstAsync();
            _currentUser = new()
            {
                Id = currentUser.Id,
                AvatarImage = currentUser.AvatarImage,
                UserName = currentUser.UserName,
            };

            _initDbData = true;
        }
    }
}
