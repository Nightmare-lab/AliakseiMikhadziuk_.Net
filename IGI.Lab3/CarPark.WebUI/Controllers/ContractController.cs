using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarPark.BLL.Services;
using CarPark.WebUI.Areas.Identity;
using CarPark.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contract = CarPark.BLL.Models.Contract;

namespace CarPark.WebUI.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class ContractController : Controller
    {
        private readonly ContractService _contractService;
        private readonly IMapper _mapper;
        private readonly ILogger<ContractController> _logger;

        public ContractController(ContractService contractService, IMapper mapper, ILogger<ContractController> logger)
        {
            _contractService = contractService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var contractView = _mapper.Map<IEnumerable<ContractViewModel>>(await _contractService.GetAllAsync());
            return View(contractView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContractViewModel contractViewModel)
        {
            try
            {
                await _contractService.AddAsync(_mapper.Map<Contract>(contractViewModel));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during creating contract. Exception: {ex.Message}");
                return View(contractViewModel);
            }

        }


        public ActionResult Create()
        {

            return View();
        }


        public async Task<IActionResult> Edit(int id)
        {
            var contractView = _mapper.Map<ContractViewModel>(await _contractService.GetAsync(id));
            return View(contractView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContractViewModel contractViewModel)
        {
            try
            {
                await _contractService.EditAsync(_mapper.Map<Contract>(contractViewModel));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during updating contract. Exception: {ex.Message}");
                return View(contractViewModel);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var contractView = _mapper.Map<ContractViewModel>(await _contractService.GetAsync(id));
            return View(contractView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ContractViewModel contractViewModel)
        {
            try
            {
                await _contractService.RemoveAsync(contractViewModel.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during deleting contract. Exception: {ex.Message}");
                return View(contractViewModel);
            }
        }
    }
}