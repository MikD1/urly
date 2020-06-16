using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Urly.Application;
using Urly.Domain;

namespace Urly.WebApi
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
            services.AddAutoMapper(typeof(DtoMappingProfile));
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ILinksRepository, LinksRepository>();
            services.AddMediatR(Assembly.GetAssembly(typeof(AppDbContext)));
            services.AddSwaggerDocumentation();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionMiddleware();
            app.UseSwaggerDocumentation();
            /*app.UseRedirectMiddleware();*/
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // WARN! Not working with migrations.
            // dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}
