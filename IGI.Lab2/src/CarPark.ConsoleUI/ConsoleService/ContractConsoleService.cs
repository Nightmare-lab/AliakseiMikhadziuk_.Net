using System;
using System.Linq;
using CarPark.BLL.Dto;
using CarPark.BLL.Service;
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

        public void ConsoleMenu()
        {
            try
            {
                Console.Clear();
                ConsolePrintMenu();
                ConsolePrintAll();

                var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                switch (menuTab)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Remove();
                        break;
                    case 3:
                        Edit();
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

        public void ConsolePrintMenu()
        {
            Console.WriteLine("1. Add contract");
            Console.WriteLine("2. Delete contract");
            Console.WriteLine("3. Edit contract ");
            Console.WriteLine("4. Back");
        }

        public void ConsolePrintAll()
        {
            var items = _contractService.GetAll().Select(contract => new ContractViewModel()
            {
                CarId = contract.CarId,
                CarName = _carService.Get(contract.CarId).CarMake,
                ContractDays = contract.ContractDays,
                EndTimeContract = contract.EndTimeContract,
                Id = contract.Id,
                StarTimeContract = contract.StarTimeContract,
            });
            items.ToTable();
        }

        public void Add()
        {
            var contractDto = Create();
            _contractService.Add(contractDto);
        }

        public void Remove()
        {
            Console.WriteLine("Enter Id of contract to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _contractService.Remove(id);
        }

        public void Edit()
        {
            Console.WriteLine("Enter Id of contract to edit");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var contractDto = Create();
            contractDto.Id = id;

            _contractService.Edit(contractDto);
        }

        public Contract Create()
        {
            Console.WriteLine("Enter Contract Start Time : ");
            var timeStart = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Contract End Time: ");
            var timeEnd = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter car Id: ");
            var car = _carService.Get(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()));
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