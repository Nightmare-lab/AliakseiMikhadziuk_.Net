namespace CarPark.BLL.Dto
{
    public class Car
    {
        public int Id { get; set; }

        public string Color { get; set; }

        public bool Rented { get; set; }

        public string CarRegistrationNumber { get; set; }

        public decimal Price { get; set; }

        public string Class { get; set; }

        public string Model { get; set; }

        public string CarMake { get; set; }

        public override string ToString()
        {
            return CarMake;
        }
    }
}