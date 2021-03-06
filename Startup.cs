using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Context;
using WebApplication.Models;
using WebApplication.Options;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication
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
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(JwtSettings), jwtSettings);

            services.AddSingleton(jwtSettings);

            var mongoOptions = Configuration.GetSection(nameof(ProductStoreDatabaseSettings));
            services.AddSingleton(sp =>
            {
                var mongoOptions = sp.GetService<IOptions<ProductStoreDatabaseSettings>>();
                return new StoreDbContext(mongoOptions);
            });
            services.Configure<ProductStoreDatabaseSettings>(opts =>
            {
                opts.ConnectionString = mongoOptions[nameof(ProductStoreDatabaseSettings.ConnectionString)];
                opts.ProductsCollectionName = mongoOptions[nameof(ProductStoreDatabaseSettings.ProductsCollectionName)];
                opts.DatabaseName = mongoOptions[nameof(ProductStoreDatabaseSettings.DatabaseName)];
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<ICartsRepo, CartsRepo>();
            services.AddSingleton<IProductsRepo, ProductsRepo>();
            services.AddSingleton<IUsersRepo, UsersRepo>();

            services.AddSingleton<IUsersService, UsersService>();
            services.AddSingleton<ICartsService, CartsService>();
            services.AddSingleton<IProductsService, ProductsService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true    
                    };
                });
            services.AddControllers(setupAction => { setupAction.ReturnHttpNotAcceptable = true; })
                .AddXmlDataContractSerializerFormatters();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appHandler =>
                {
                    appHandler.Run(async context =>
                        {
                            context.Response.StatusCode = 500;
                            await context.Response.WriteAsync("Something went wrong please try again later");
                        }
                    );
                });
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}