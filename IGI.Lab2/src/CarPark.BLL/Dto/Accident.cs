﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPark.BLL.Dto
{
    public class Accident
    {
        public int Id { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }

        public Contract Contract { get; set; }

        public DateTime DateTrafficAccident { get; set; }

        public string Collisions { get; set; }

        public string Result { get; set; }

        public override string ToString()
        {
            return Contract.Car.CarMake;
        }
    }
}