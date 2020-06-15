using Microsoft.EntityFrameworkCore;
using Urly.Domain;

namespace Urly.Application
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>()
                .ToTable("links")
                .Ignore(x => x.ShortCode);
        }
    }
}
