using CarPark.DAL.Interfaces;

namespace CarPark.DAL.Models
{
    public class Cars : IEntity
    {
        public int Id { get; set; }

        public CarModels Models { get; set; }

        public string Color { get; set; }

        public bool Rented { get; set; }

        public string CarRegistrationNumber { get; set; }

        public decimal Price { get; set; }
    }
}