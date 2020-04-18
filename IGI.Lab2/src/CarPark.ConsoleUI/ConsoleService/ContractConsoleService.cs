using System;
using System.Linq;
using System.Threading.Tasks;
using CarPark.BLL.Models;
using CarPark.BLL.Services;
using CarPark.ConsoleUI.Extensions;
using CarPark.ConsoleUI.Interfaces;
using CarPark.ConsoleUI.ViewModels;

namespace CarPark.ConsoleUI.ConsoleService
{
    public class ContractConsoleService : IConsoleService, ICrudService<Contract>
    {
        private readonly ContractService _contractService;
        private readonly CarService _carService;

        public ContractConsoleService(ContractService contractService, CarService carService)
        {
            _contractService = contractService;
            _carService = carService;
        }

        public async Task StartMenu()
        {
            try
            {
                Console.Clear();
                PrintMenu();
                await PrintItems();

                var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                switch (menuTab)
                {
                    case 1:
                        await AddAsync();
                        break;
                    case 2:
                        await RemoveAsync();
                        break;
                    case 3:
                        await EditAsync();
                        break;
                    case 4:
                        return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. AddAsync contract");
            Console.WriteLine("2. Delete contract");
            Console.WriteLine("3. EditAsync contract ");
            Console.WriteLine("4. Back");
        }

        public async Task PrintItems()
        {
            var items = (await _contractService.GetAllAsync()).Select( async contract =>   new ContractViewModel()
            {
                CarId = contract.CarId,
                CarName = (await _carService.GetAsync(contract.CarId)).CarMake,
                ContractDays = contract.ContractDays,
                EndTimeContract = contract.EndTimeContract,
                Id = contract.Id,
                StarTimeContract = contract.StarTimeContract,
            });
            items.ToTable();
        }

        public async Task AddAsync()
        {
            var contractDto = await CreateAsync();
            await _contractService.AddAsync(contractDto);
        }

        public async Task RemoveAsync()
        {
            Console.WriteLine("Enter Id of contract to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _contractService.RemoveAsync(id);
        }

        public async Task EditAsync()
        {
            Console.WriteLine("Enter Id of contract to edit");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var contractDto = await CreateAsync();
            contractDto.Id = id;

            await _contractService.EditAsync(contractDto);
        }

        public async Task<Contract> CreateAsync()
        {
            Console.WriteLine("Enter Contract Start Time : ");
            var timeStart = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Contract End Time: ");
            var timeEnd = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter car Id: ");
            var car = await _carService.GetAsync(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()));
            Console.WriteLine("Enter the number of days: ");
            var numberOfDay = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var contractDto = new Contract()
            {
                StarTimeContract = timeStart,
                EndTimeContract = timeEnd,
                CarId = car.Id,
                ContractDays = numberOfDay,
            };

            return contractDto;
        }
    }
}