using A3System.Dbo.Model;
using Microsoft.EntityFrameworkCore;

namespace A3System.Dbo
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SetorModel>().HasKey(c => c.Id);
            builder.Entity<UserModel>().HasKey(c => c.Id);
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<SetorModel> Setor { get; set; }
    }
}