using BookActivity.Api.Common.Extensions;
using BookActivity.Api.Middleware;
using BookActivity.Shared.Models;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace BookActivity.Api
{
    public class Startup
    {
        public readonly IConfiguration Configuration;
        public readonly IWebHostEnvironment Environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookActivity.Api", Version = "v1" });
            });

            services.AddScoped(s =>
            {
                var httpContextAccessor = s.GetRequiredService<IHttpContextAccessor>();
                var user = httpContextAccessor.HttpContext.Items["User"];

                return user as CurrentUser;
            });

            services.ConfigureAuthentication(Configuration);
            services.Configure<TokenInfo>(Configuration.GetSection(typeof(TokenInfo).Name));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.ConfigureModules(Configuration);
            services.AddCors();
            services.AddSignalR();
            services.AddLogging();
            services.AddEndpointsApiExplorer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookActivity.Api v1"));
            }

            CreateDatabasesIfNotExist(app);

            app.UseHttpsRedirection();
            app.UseCors(x => x.WithOrigins(Configuration.GetValue<string>("ClientAddress"))
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());

            app.UseRouting();
            app.UseAuthorization();
            app.UseHttpLogging();
            app.UseMiddleware<JwtMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
                Infrastructure.ModuleConfiguration.ConfigureSignalREndpoints(endpoints);
            });
        }

        private void CreateDatabasesIfNotExist(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            Infrastructure.Data.ModuleConfiguration infraModuleConfiguration = new();
            infraModuleConfiguration.CreateDatabasesIfNotExist(serviceScope);
        }
    }
}
