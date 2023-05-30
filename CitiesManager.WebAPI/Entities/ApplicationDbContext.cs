using CitiesManager.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;

namespace CitiesManager.WebAPI.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public ApplicationDbContext()
        {
            
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(new City() {CityId = Guid.Parse("6DB58990-8BB2-47A6-B92B-B927A22E4098"), CityName = "New York" });
            modelBuilder.Entity<City>().HasData(new City() { CityId = Guid.Parse("D02ECDE9-66B0-4BDC-A2F9-FC119CB2C495"), CityName = "Istanbul" });
        }
    }
}
