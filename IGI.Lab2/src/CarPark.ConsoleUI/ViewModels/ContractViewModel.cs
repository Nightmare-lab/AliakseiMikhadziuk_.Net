using System;

namespace CarPark.ConsoleUI.ViewModels
{
    public class ContractViewModel
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public DateTime StarTimeContract { get; set; }

        public DateTime EndTimeContract { get; set; }

        public string CarName { get; set; }

        public int ContractDays { get; set; }
    }
}