using System.ComponentModel.DataAnnotations;

namespace CarPark.WebUI.ViewModels
{
    public class CarViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Color not specified!")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Car is rented not specified!")]
        public bool Rented { get; set; }

        [Required(ErrorMessage = "Car registration number not specified!")]
        public string CarRegistrationNumber { get; set; }

        [Required(ErrorMessage = "Price not specified!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Class not specified!")]
        public string Class { get; set; }

        [Required(ErrorMessage = "Model not specified!")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Car make not specified!")]
        public string CarMake { get; set; }
    }
}
