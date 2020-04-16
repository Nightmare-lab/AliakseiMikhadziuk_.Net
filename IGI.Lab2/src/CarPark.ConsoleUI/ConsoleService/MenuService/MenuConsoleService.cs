using System;
using CarPark.ConsoleUI.Interfaces;

namespace CarPark.ConsoleUI.ConsoleService.MenuService
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

        public void ConsoleMenu()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    ConsolePrintMenu();

                    var menuItemSelection = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuItemSelection)
                    {
                        case 1:
                            {
                                _carsConsoleService.ConsoleMenu();
                            }

                            break;
                        case 2:
                            {
                                _contractConsoleService.ConsoleMenu();
                            }

                            break;
                        case 3:
                            {
                                _accidentConsoleService.ConsoleMenu();
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

        public void ConsolePrintMenu()
        {
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Contract");
            Console.WriteLine("3. Accident");
            Console.WriteLine("4. Exit");
        }

        public void ConsolePrintAll()
        {

        }
    }
}
