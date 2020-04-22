using CarPark.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPark.DAL.EF
{
    public class CarParkContext : DbContext
    {
        public CarParkContext(DbContextOptions<CarParkContext> options)
             : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Accident> Accidents { get; set; }
    }
}