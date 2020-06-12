using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Urly.Application;
using Urly.Domain;

namespace Urly.IntegrationTests
{
    public class UrlyWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor descriptor =
                    services.Single(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("UrlyInMemoryTestDb"); });

                ServiceProvider sp = services.BuildServiceProvider();
                using IServiceScope scope = sp.CreateScope();
                IServiceProvider scopedServices = scope.ServiceProvider;
                AppDbContext db = scopedServices.GetRequiredService<AppDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                SeedTestData(db);
            });
        }

        private void SeedTestData(AppDbContext dbContext)
        {
            dbContext.Links.Add(new Link("https://urly.dev/abc")); // b
            dbContext.Links.Add(new Link("http://urly.dev/123")); // c
            dbContext.Links.Add(new Link("urly/xyz")); // d
            dbContext.SaveChanges();
        }
    }
}
