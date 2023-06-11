using Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            base.OnModelCreating(modelBuilder);
        }
    }
}