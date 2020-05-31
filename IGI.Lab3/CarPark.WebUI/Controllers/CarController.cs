using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarPark.BLL.Models;
using CarPark.BLL.Services;
using CarPark.WebUI.Areas.Identity;
using CarPark.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarPark.WebUI.Controllers
{
    
    public class CarController : Controller
    {
        private readonly CarService _carService;
        private readonly IMapper _mapper;
        private readonly ILogger<CarController> _logger;

        public CarController(CarService carService, IMapper mapper, ILogger<CarController> logger)
        {
            _carService = carService;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            var carView =  _mapper.Map<IEnumerable<CarViewModel>>(await _carService.GetAllAsync());
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
                await _carService.AddAsync(_mapper.Map<Car>(carViewModel));
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occured during creating car. Exception: {ex.Message}");
                return View(carViewModel);
            }
        }


        
        public async Task<IActionResult> Edit(int id)
        {
            var carView = _mapper.Map<CarViewModel>(await _carService.GetAsync(id));
            return View(carView);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarViewModel carViewModel)
        {
            try
            {
                await _carService.EditAsync(_mapper.Map<Car>(carViewModel));
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occured during updating car. Exception: {ex.Message}");
                return View(carViewModel);
            }
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var carView = _mapper.Map<CarViewModel>(await _carService.GetAsync(id));
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
            catch(Exception ex)
            {
                _logger.LogError($"Error occured during deleting car. Exception: {ex.Message}");
                return View(carViewModel);
            }
        }
    }
}