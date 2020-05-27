using CarPark.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarPark.DAL.EF
{
    public class CarParkContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ILoggerFactory _loggerFactory;
        public CarParkContext(DbContextOptions<CarParkContext> options, ILoggerFactory loggerFactory)
             : base(options)
        {
            _loggerFactory = loggerFactory;
            Database.Migrate();
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Accident> Accidents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}