using CarPark.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPark.DAL.EF
{
    public class CarParkContext : DbContext
    {
        public CarParkContext(DbContextOptions<CarParkContext> options)
             : base(options)
        {
            
          
        }

        public DbSet<Cars> Cars { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<CarModels> CarModels { get; set; }
        public DbSet<Accidents> Accidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>().HasData(new Cars
            {
                Color = "Black",
                Rented = true,
                CarRegistrationNumber = "AH509B",
                Price = 20,
                Models = new CarModels()
                {
                    Class = "Business",
                    Model = "Logan",
                    CarMake = "Reno"
                }
            });
            base.OnModelCreating(modelBuilder);
        }
    }

}