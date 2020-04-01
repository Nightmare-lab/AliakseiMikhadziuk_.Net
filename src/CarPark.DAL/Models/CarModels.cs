using CarPark.DAL.Interfaces;

namespace CarPark.DAL.Models
{
    public class CarModels : IEntity
    {
        public int Id { get; set; }

        public string Class { get; set; }

        public string Model { get; set; }

        public string CarMake { get; set; }
    }
}