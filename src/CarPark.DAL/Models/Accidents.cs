using System;
using CarPark.DAL.Interfaces;

namespace CarPark.DAL.Models
{
    public class Accidents : IEntity
    {
        public int Id { get; set; }

        public Contracts ContractId { get; set; }

        public DateTime DateTrafficAccident { get; set; }

        public string Collisions { get; set; }

        public string Result { get; set; }

    }
}