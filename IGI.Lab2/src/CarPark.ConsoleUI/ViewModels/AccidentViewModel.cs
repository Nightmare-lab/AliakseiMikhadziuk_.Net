using System;

namespace CarPark.ConsoleUI.ViewModels
{
    public class AccidentViewModel
    {
        public int ContractId { get; set; }

        public DateTime DateTrafficAccident { get; set; }

        public string Collisions { get; set; }

        public string Result { get; set; }
    }
}