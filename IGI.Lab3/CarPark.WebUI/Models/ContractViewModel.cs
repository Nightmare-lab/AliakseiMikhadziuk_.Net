using System;
using System.ComponentModel.DataAnnotations;
using CarPark.DAL.Models;

namespace CarPark.WebUI.Models
{
    public class ContractViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Start time contract not specified!")]
        public DateTime StarTimeContract { get; set; }

        [Required(ErrorMessage = "End time contract not specified!")]
        public DateTime EndTimeContract { get; set; }

        [Required(ErrorMessage = "CarId not specified!")]
        public int CarId { get; set; }

        [Required]
        public Car Car { get; set; }

        [Required(ErrorMessage = "Contract days not specified!")]
        public int ContractDays { get; set; }
    }
}
