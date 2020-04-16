using System;

namespace CarPark.BLL.Dto
{
    public class Contract
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public DateTime StarTimeContract { get; set; }

        public DateTime EndTimeContract { get; set; }

        public Car Car { get; set; }

        public int ContractDays { get; set; }

        public override string ToString()
        {
            return Car.CarMake;
        }
    }
}