using BookActivity.Api.Common.Extensions;
using BookActivity.Api.Middleware;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookActivity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

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

                return user != null ? (user as AppUserDto) : null;
            });

            AddAuthentication(services);

            services.Configure<TokenInfo>(Configuration.GetSection(typeof(TokenInfo).Name));

            services.AddMediatR(typeof(Startup));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
            });
        }

        private void CreateDatabasesIfNotExist(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            Infrastructure.Data.ModuleConfiguration infraModuleConfiguration = new();
            infraModuleConfiguration.CreateDatabasesIfNotExist(serviceScope);
        }

        private void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "BookActivityServer",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSecretKey())),
                    ValidAudience = "BookActivityClient",
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });
        }
    }
}
