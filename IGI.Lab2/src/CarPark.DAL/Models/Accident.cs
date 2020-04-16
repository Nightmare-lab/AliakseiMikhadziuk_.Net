using System;
using System.ComponentModel.DataAnnotations.Schema;
using CarPark.DAL.Interfaces;

namespace CarPark.DAL.Models
{
    public class Accident : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }

        public Contract Contract { get; set; }

        public DateTime DateTrafficAccident { get; set; }

        public string Collisions { get; set; }

        public string Result { get; set; }
    }
}