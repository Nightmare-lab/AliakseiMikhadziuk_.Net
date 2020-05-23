using System.Linq;
using System.Threading.Tasks;
using CarPark.BLL.Services;
 using CarPark.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
 using Contract = CarPark.BLL.Models.Contract;

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

        
        public async Task<IActionResult> Edit(int id)
        {
            var contract = await _contractService.GetAsync(id);

            var contractView = new ContractViewModel()
            {
                Id = id,
                StarTimeContract = contract.StarTimeContract,
                EndTimeContract = contract.EndTimeContract,
                ContractDays = contract.ContractDays,
                CarId = contract.CarId
            };
            return View(contractView);
        }

        // POST: Contract/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContractViewModel contractViewModel)
        {
            try
            {
                await _contractService.EditAsync(new Contract()
                {
                    Id = id,
                    StarTimeContract = contractViewModel.StarTimeContract,
                    EndTimeContract = contractViewModel.EndTimeContract,
                    ContractDays = contractViewModel.ContractDays,
                    CarId = contractViewModel.CarId
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(contractViewModel);
            }
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _contractService.GetAsync(id);

            var contractView = new ContractViewModel()
            {
                Id = contract.Id,
                CarId = contract.CarId,
                StarTimeContract = contract.StarTimeContract,
                EndTimeContract = contract.EndTimeContract,
                ContractDays = contract.ContractDays
            };

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
            catch
            {
                return View(contractViewModel);
            }
        }
    }
}