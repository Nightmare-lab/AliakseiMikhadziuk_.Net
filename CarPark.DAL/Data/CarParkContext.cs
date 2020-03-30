using Microsoft.EntityFrameworkCore;

namespace CarPark.DAL.Data
{
    public class CarParkContext : DbContext
    {
        public CarParkContext(DbContextOptions<CarParkContext> options)
             : base(options)
        {
          
        }
    }

}