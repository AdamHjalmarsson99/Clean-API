using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MySQLDb
{
    public class RealDatabase : DbContext
    {
        public RealDatabase() { }

        public RealDatabase(DbContextOptions<RealDatabase> options) : base(options) { }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Bird> Birds { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=animals;user=root;password=MorrisZonya99!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //DatabaseSeedHelper.SeedData(modelBuilder);
        }

    }
}
