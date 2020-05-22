using System.Linq;
using System.Threading.Tasks;
using CarPark.BLL.Models;
using CarPark.BLL.Services;
using CarPark.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.WebUI.Controllers
{
    public class AccidentController : Controller
    {
        private readonly AccidentService _accidentService;

        public AccidentController(AccidentService accidentService)
        {
            _accidentService = accidentService;
        }
        
        public async Task<IActionResult> Index()
        {
            var accidentView = (await _accidentService.GetAllAsync()).Select(e => new AccidentViewModel()
            {
                Id = e.Id,
                Collisions = e.Collisions,
                ContractId = e.ContractId,
                DateTrafficAccident = e.DateTrafficAccident,
                Result = e.Result,
                Contract = e.Contract
            });
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
                await _accidentService.AddAsync(new Accident()
                {
                    Collisions = accidentViewModel.Collisions,
                    Result = accidentViewModel.Result,
                    Contract = accidentViewModel.Contract,
                    ContractId = accidentViewModel.ContractId,
                    DateTrafficAccident = accidentViewModel.DateTrafficAccident
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(accidentViewModel);
            }
        }

        // GET: Accident/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Accident/Edit/5
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

        // GET: Accident/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Accident/Delete/5
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