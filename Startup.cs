using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WebApplication.Context;
using WebApplication.Models;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
            services.Configure<MoviestoreDatabaseSettings>(
                Configuration.GetSection(nameof(MoviestoreDatabaseSettings)));
            services.Configure<ProductStoreDatabaseSettings>(
                Configuration.GetSection(nameof(ProductStoreDatabaseSettings)));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<CartsRepo>();
            services.AddSingleton<ProductsRepo>();

            services.AddSingleton<IMoviestoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MoviestoreDatabaseSettings>>().Value);
            services.AddScoped<IProductStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ProductStoreDatabaseSettings>>().Value);

            services.AddSingleton<CartService>();
            services.AddSingleton<ProductsService>();

            services.AddControllers(setupAction => { setupAction.ReturnHttpNotAcceptable = true; })
                .AddXmlDataContractSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                            await context.Response.WriteAsync("Something wen wrong please try again later");
                        }
                    );
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}