using CarPark.DAL.Models;

namespace CarPark.BLL.Dto
{
    public class Cars
    {
        public int Id { get; set; }

        public CarModels Models { get; set; }

        public string Color { get; set; }

        public bool Rented { get; set; }

        public string CarRegistrationNumber { get; set; }

        public decimal Price { get; set; }
    }
}