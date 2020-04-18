using System;
using System.Threading.Tasks;
using CarPark.ConsoleUI.Interfaces;

namespace CarPark.ConsoleUI.ConsoleService
{
    public class MenuConsoleService : IConsoleService
    {
        private readonly CarsConsoleService _carsConsoleService;
        private readonly ContractConsoleService _contractConsoleService;
        private readonly AccidentConsoleService _accidentConsoleService;

        public MenuConsoleService(
            CarsConsoleService carsConsoleService,
            ContractConsoleService contractConsoleService,
            AccidentConsoleService accidentConsoleService)
        {
            _carsConsoleService = carsConsoleService;
            _contractConsoleService = contractConsoleService;
            _accidentConsoleService = accidentConsoleService;
        }

        public async Task StartMenu()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    PrintMenu();

                    var menuItemSelection = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuItemSelection)
                    {
                        case 1:
                            {
                               await _carsConsoleService.StartMenu();
                            }

                            break;
                        case 2:
                            {
                                await _contractConsoleService.StartMenu();
                            }

                            break;
                        case 3:
                            {
                               await _accidentConsoleService.StartMenu();
                            }

                            break;
                        case 4:
                            {
                                return;
                            }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
            }
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Contract");
            Console.WriteLine("3. Accident");
            Console.WriteLine("4. Exit");
        }

        public Task PrintItems()
        {
            throw new NotImplementedException();
        }
    }
}
