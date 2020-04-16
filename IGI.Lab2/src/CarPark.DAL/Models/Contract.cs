using System;
using System.ComponentModel.DataAnnotations.Schema;
using CarPark.DAL.Interfaces;

namespace CarPark.DAL.Models
{
    public class Contract : IEntity
    {
        public int Id { get; set; }

        public DateTime StarTimeContract { get; set; }

        public DateTime EndTimeContract { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        public Car Car { get; set; }

        public int ContractDays { get; set; }
    }
}