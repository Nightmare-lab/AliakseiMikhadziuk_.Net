using System;
using CarPark.BLL.Dto;
using CarPark.BLL.Service;
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
            Console.WriteLine("1. Add car");
            Console.WriteLine("2. Delete car");
            Console.WriteLine("3. Update car");
            Console.WriteLine("4. Back");
        }

        public void ConsolePrintAll()
        {
            var items = _carService.GetAll();
            items.ToTable();
        }

        public void Add()
        {
            var carDto = Create();
            _carService.Add(carDto);
        }

        public void Remove()
        {
            Console.WriteLine("Enter Id of car to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _carService.Remove(id);
        }

        public void Edit()
        {
            Console.WriteLine("Enter Id of car to edit");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var carDto = Create();
            carDto.Id = id;

            _carService.Edit(carDto);
        }

        public Car Create()
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