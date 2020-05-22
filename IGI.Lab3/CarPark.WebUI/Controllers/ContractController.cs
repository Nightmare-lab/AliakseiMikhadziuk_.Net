 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using CarPark.BLL.Models;
 using CarPark.BLL.Services;
 using CarPark.WebUI.Models;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.WebUI.Controllers
{
    public class ContractController : Controller
    {
        private readonly ContractService _contractService;

        public ContractController(ContractService contractService)
        {
            _contractService = contractService;
        }

        public async Task<IActionResult> Index()
        {
            var contractView = (await _contractService.GetAllAsync()).Select(e => new ContractViewModel()
            {
                Id = e.Id,
                CarId = e.CarId,
                ContractDays = e.ContractDays,
                StarTimeContract = e.StarTimeContract,
                EndTimeContract = e.EndTimeContract

            });
            return View(contractView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(ContractViewModel contractViewModel)
        {
            try
            {
                await _contractService.AddAsync(new Contract()
                {
                    StarTimeContract = contractViewModel.StarTimeContract,
                    ContractDays = contractViewModel.ContractDays,
                    EndTimeContract = contractViewModel.EndTimeContract,
                    CarId = contractViewModel.CarId
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(contractViewModel);
            }

        }

        
        public ActionResult Create()
        {
           
                return View();
        }

        // GET: Contract/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contract/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contract/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contract/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}