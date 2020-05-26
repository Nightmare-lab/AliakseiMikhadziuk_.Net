using System;
using System.ComponentModel.DataAnnotations;

namespace CarPark.WebUI.ViewModels
{
    public class AccidentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Id not specified!")]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "Date traffic accident not specified!")]
        public DateTime DateTrafficAccident { get; set; }

        [Required(ErrorMessage = "Collision not specified!")]
        public string Collisions { get; set; }

        [Required(ErrorMessage = "Result not specified!")]
        public string Result { get; set; }
    }
}
