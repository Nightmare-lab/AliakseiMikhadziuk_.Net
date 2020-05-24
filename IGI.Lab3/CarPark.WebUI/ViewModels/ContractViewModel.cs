using System;
using System.ComponentModel.DataAnnotations;

namespace CarPark.WebUI.ViewModels
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

        [Required(ErrorMessage = "Contract days not specified!")]
        public int ContractDays { get; set; }
    }
}
