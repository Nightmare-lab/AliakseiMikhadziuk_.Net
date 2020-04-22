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
    public class AccidentConsoleService : IConsoleService, ICrudService<Accident>
    {
        private readonly AccidentService _accidentService;
        private readonly ContractService _contractService;

        public AccidentConsoleService(AccidentService accidentService, ContractService contractService)
        {
            _accidentService = accidentService;
            _contractService = contractService;
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
            Console.WriteLine("1. AddAsync accidents");
            Console.WriteLine("2. Delete accidents");
            Console.WriteLine("3. EditAsync accidents ");
            Console.WriteLine("4. Back");
        }

        public async Task PrintItems()
        {
            var items = (await _accidentService.GetAllAsync()).Select(accident => new AccidentViewModel()
            {
                ContractId = accident.Id,
                DateTrafficAccident = accident.DateTrafficAccident,
                Collisions = accident.Collisions,
                Result = accident.Result,
            });
            items.ToTable();
        }

        public async Task AddAsync()
        {
            var accidentDto = await CreateAsync();
            await _accidentService.AddAsync(accidentDto);
        }

        public async Task RemoveAsync()
        {
            Console.WriteLine("Enter Id of accident to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _accidentService.RemoveAsync(id);
        }

        public async Task EditAsync()
        {
            Console.WriteLine("Enter Id of accident to edit");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var accidentDto = await CreateAsync();
            accidentDto.Id = id;

            await _accidentService.EditAsync(accidentDto);
        }

        public async Task<Accident> CreateAsync()
        {
            Console.WriteLine("Enter contract id: ");
            var contract = await _contractService.GetAsync(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()));
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