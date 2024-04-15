using LoginApp.Services.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.Services.Identity.Infrastructure
{
    public class IdentityDbContext: DbContext
    {

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.UserName);
        }

        public DbSet<User> Users { get; set; }
    }
}
