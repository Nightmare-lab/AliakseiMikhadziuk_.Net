using CarPark.DAL.Interfaces;

namespace CarPark.DAL.Models
{
    public class Car : IEntity
    {
        public int Id { get; set; }

        public string Color { get; set; }

        public bool Rented { get; set; }

        public string CarRegistrationNumber { get; set; }

        public decimal Price { get; set; }

        public string Class { get; set; }

        public string Model { get; set; }

        public string CarMake { get; set; }
    }
}