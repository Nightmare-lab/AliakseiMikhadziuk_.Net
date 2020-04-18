using System;
using System.Threading.Tasks;
using CarPark.BLL.Models;
using CarPark.BLL.Services;
using CarPark.ConsoleUI.Extensions;
using CarPark.ConsoleUI.Interfaces;
using static System.Decimal;

namespace CarPark.ConsoleUI.ConsoleService
{
    public class CarsConsoleService : IConsoleService, ICrudService<Car>
    {
        private readonly CarService _carService;

        public CarsConsoleService(CarService carService)
        {
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
            Console.WriteLine("1. AddAsync car");
            Console.WriteLine("2. Delete car");
            Console.WriteLine("3. Update car");
            Console.WriteLine("4. Back");
        }

        public async Task PrintItems()
        {
            var items = await _carService.GetAllAsync();
            items.ToTable();
        }

        public async Task AddAsync()
        {
            var carDto = await CreateAsync();
            await _carService.AddAsync(carDto);
        }

        public async Task RemoveAsync()
        {
            Console.WriteLine("Enter Id of car to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _carService.RemoveAsync(id);
        }

        public async Task EditAsync()
        {
            Console.WriteLine("Enter Id of car to edit");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var carDto = await CreateAsync();
            carDto.Id = id;

           await _carService.EditAsync(carDto);
        }

        public async Task<Car> CreateAsync()
        {
            Console.WriteLine("Enter car make: ");
            var carMake = Console.ReadLine();
            Console.WriteLine("Enter car model: ");
            var model = Console.ReadLine();
            Console.WriteLine("Enter car class: ");
            var carClass = Console.ReadLine();
            Console.WriteLine("Enter car registration number: ");
            var carRegisrNamber = Console.ReadLine();
            Console.WriteLine("Enter car color: ");
            var carColor = Console.ReadLine();
            Console.WriteLine("Enter is car rented? ");
            var isCarRented = bool.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            Console.WriteLine("Enter car price $: ");
            var carPrice = Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var carDto = new Car()
            {
                CarMake = carMake,
                Model = model,
                Class = carClass,
                Color = carColor,
                Rented = isCarRented,
                CarRegistrationNumber = carRegisrNamber,
                Price = carPrice,
            };

            return carDto;
        }

    }
}