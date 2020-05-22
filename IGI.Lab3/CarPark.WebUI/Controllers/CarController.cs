using System.Linq;
using System.Threading.Tasks;
using CarPark.BLL.Models;
using CarPark.BLL.Services;
using CarPark.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }
        
        public async Task<IActionResult> Index()
        {
            var carView = (await _carService.GetAllAsync()).Select(e => new CarViewModel()
            {
                Id = e.Id,
                CarMake = e.CarMake,
                CarRegistrationNumber = e.CarRegistrationNumber,
                Class = e.Class,
                Color = e.Color,
                Model = e.Model,
                Price = e.Price,
                Rented = e.Rented
            });
            return View(carView);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarViewModel carViewModel)
        {
            try
            {
                await _carService.AddAsync(new Car
                {
                    
                    CarMake = carViewModel.CarMake,
                    CarRegistrationNumber = carViewModel.CarRegistrationNumber,
                    Class = carViewModel.Class,
                    Color = carViewModel.Color,
                    Model = carViewModel.Model,
                    Price = carViewModel.Price,
                    Rented = carViewModel.Rented
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(carViewModel);
            }
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.GetAsync(id);

            var carView = new CarViewModel()
            {
                Id = id,
                CarMake = car.CarMake,
                CarRegistrationNumber = car.CarRegistrationNumber,
                Class = car.Class,
                Color = car.Color,
                Model = car.Model,
                Price = car.Price,
                Rented = car.Rented
            };
            return View(carView);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarViewModel carViewModel,int id)
        {
            try
            {
                await _carService.EditAsync(new Car()
                {
                    Id = id,
                    CarMake = carViewModel.CarMake,
                    CarRegistrationNumber = carViewModel.CarRegistrationNumber,
                    Class = carViewModel.Class,
                    Color = carViewModel.Color,
                    Model = carViewModel.Model,
                    Price = carViewModel.Price,
                    Rented = carViewModel.Rented
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(carViewModel);
            }
        }

        
        public async Task<IActionResult> Delete(int id)
        {

            var car = await _carService.GetAsync(id);

            var carView = new CarViewModel()
            {
                Id = car.Id,
                CarMake = car.CarMake,
                CarRegistrationNumber = car.CarRegistrationNumber,
                Class = car.Class,
                Color = car.Color,
                Model = car.Model,
                Price = car.Price,
                Rented = car.Rented
            };
            return View(carView);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CarViewModel carViewModel)
        {
            try
            {
                await _carService.RemoveAsync(carViewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(carViewModel);
            }
        }
    }
}