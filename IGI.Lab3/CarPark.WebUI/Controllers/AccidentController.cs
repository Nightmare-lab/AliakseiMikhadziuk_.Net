using System.Linq;
using System.Threading.Tasks;
using CarPark.BLL.Models;
using CarPark.BLL.Services;
using CarPark.WebUI.Models;
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

       
        public  async Task<IActionResult> Edit(int id)
        {
            var accident = await _accidentService.GetAsync(id);

            var accidentView = new AccidentViewModel()
            {
                Id = id,
                Collisions = accident.Collisions,
                ContractId = accident.ContractId,
                DateTrafficAccident = accident.DateTrafficAccident,
                Result = accident.Result
            };

            return View(accidentView);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AccidentViewModel accidentViewModel)
        {
            try
            {
                await _accidentService.EditAsync(new Accident()
                {
                    Id = id,
                    Collisions = accidentViewModel.Collisions,
                    ContractId = accidentViewModel.ContractId,
                    DateTrafficAccident = accidentViewModel.DateTrafficAccident,
                    Result = accidentViewModel.Result
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(accidentViewModel);
            }
        }

        
        public  async Task<IActionResult> Delete(int id)
        {
            var accident = await _accidentService.GetAsync(id);

            var accidentView = new AccidentViewModel()
            {
                Id = accident.Id,
                Collisions = accident.Collisions,
                ContractId = accident.ContractId,
                DateTrafficAccident = accident.DateTrafficAccident,
                Result = accident.Result
            };
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
            catch
            {
                return View(accidentViewModel);
            }
        }
    }
}