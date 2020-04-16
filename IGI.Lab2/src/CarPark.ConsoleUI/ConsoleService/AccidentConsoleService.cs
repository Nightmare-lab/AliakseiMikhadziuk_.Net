using System;
using System.Linq;
using CarPark.BLL.Dto;
using CarPark.BLL.Service;
using CarPark.ConsoleUI.Extensions;
using CarPark.ConsoleUI.Interfaces;
using CarPark.ConsoleUI.ViewModels;

namespace CarPark.ConsoleUI.ConsoleService
{
    public class AccidentConsoleService : IConsoleService, ICrudService<Accident>
    {
        private readonly AccidentService _accidentService;
        private readonly ContractService _contractService;

        public AccidentConsoleService(AccidentService accidentService, ContractService contractService)
        {
            _accidentService = accidentService;
            _contractService = contractService;
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
            Console.WriteLine("1. Add accidents");
            Console.WriteLine("2. Delete accidents");
            Console.WriteLine("3. Edit accidents ");
            Console.WriteLine("4. Back");
        }

        public void ConsolePrintAll()
        {
            var items = _accidentService.GetAll().Select(accident => new AccidentViewModel()
            {
                ContractId = accident.Id,
                DateTrafficAccident = accident.DateTrafficAccident,
                Collisions = accident.Collisions,
                Result = accident.Result,
            });
            items.ToTable();
        }

        public void Add()
        {
            var accidentDto = Create();
            _accidentService.Add(accidentDto);
        }

        public void Remove()
        {
            Console.WriteLine("Enter Id of accident to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _accidentService.Remove(id);
        }

        public void Edit()
        {
            Console.WriteLine("Enter Id of accident to edit");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var accidentDto = Create();
            accidentDto.Id = id;

            _accidentService.Edit(accidentDto);
        }

        public Accident Create()
        {
            Console.WriteLine("Enter contract id: ");
            var contract = _contractService.Get(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()));
            Console.WriteLine("Enter Date Traffic Accident: ");
            var timeTrafficTime = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter collision: ");
            var collision = Console.ReadLine();
            Console.WriteLine("Enter result: ");
            var result = Console.ReadLine();

            var accidentDto = new Accident()
            {
                ContractId = contract.Id,
                DateTrafficAccident = timeTrafficTime,
                Collisions = collision,
                Result = result,
            };

            return accidentDto;
        }
    }
}