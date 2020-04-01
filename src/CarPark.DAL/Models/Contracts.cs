using System;
using CarPark.DAL.Interfaces;

namespace CarPark.DAL.Models
{
    public class Contracts : IEntity
    {
        public int Id { get; set; }

        public DateTime StarTimeContract { get; set; }

        public DateTime EndTimeContract { get; set; }

        public Cars CarId { get; set; }

        public int ContractDays { get; set; }

        public decimal SummaryPrice { get; set; }
    }
}