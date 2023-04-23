using BookActivity.Api.Common.Extensions;
using BookActivity.Api.Middleware;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Domain.Hubs;
using BookActivity.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace BookActivity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
                endpoints.MapHub<UserNotificationsHub>("/userNotificationsHub");
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
