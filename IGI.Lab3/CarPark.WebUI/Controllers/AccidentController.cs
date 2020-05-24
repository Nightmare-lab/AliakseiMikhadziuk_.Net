using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarPark.BLL.Models;
using CarPark.BLL.Services;
using CarPark.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarPark.WebUI.Controllers
{
    public class AccidentController : Controller
    {
        private readonly AccidentService _accidentService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccidentController> _logger;

        public AccidentController(AccidentService accidentService, IMapper mapper, ILogger<AccidentController> logger)
        {
            _accidentService = accidentService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var accidentView = _mapper.Map<IEnumerable<AccidentViewModel>>(await _accidentService.GetAllAsync());
            return View(accidentView);
        }



        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccidentViewModel accidentViewModel)
        {
            try
            {
                await _accidentService.AddAsync(_mapper.Map<Accident>(accidentViewModel));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during creating accident. Exception: {ex.Message}");
                return View(accidentViewModel);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var accidentView = _mapper.Map<AccidentViewModel>(await _accidentService.GetAsync(id));
            return View(accidentView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccidentViewModel accidentViewModel)
        {
            try
            {
                await _accidentService.EditAsync(_mapper.Map<Accident>(accidentViewModel));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during updating accident. Exception: {ex.Message}");
                return View(accidentViewModel);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var accidentView = _mapper.Map<AccidentViewModel>(await _accidentService.GetAsync(id));
            return View(accidentView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AccidentViewModel accidentViewModel)
        {
            try
            {
                await _accidentService.RemoveAsync(accidentViewModel.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during deleting accident. Exception: {ex.Message}");
                return View(accidentViewModel);
            }
        }
    }
}