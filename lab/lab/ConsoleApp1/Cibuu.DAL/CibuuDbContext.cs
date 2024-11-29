using Microsoft.EntityFrameworkCore;
using Cibuu.DAL.models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Cibuu.DAL
{
    public class CibuuDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = optionsBuilder.UseNpgsql("Host=localhost;Database=cibuu;Username=postgres;Password='23092005'");
;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: Add custom configurations here if necessary
        }
    }
}
