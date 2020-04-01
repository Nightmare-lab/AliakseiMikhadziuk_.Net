using System;
using CarPark.DAL.Models;

namespace CarPark.BLL.Dto
{
    public class Accidents
    {
        public int Id { get; set; }

        public Contracts ContractId { get; set; }

        public DateTime DateTrafficAccident { get; set; }

        public string Collisions { get; set; }

        public string Result { get; set; }
    }
}