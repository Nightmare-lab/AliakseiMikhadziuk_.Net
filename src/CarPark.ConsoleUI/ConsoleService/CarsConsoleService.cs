using System;
using CarPark.BLL.Service;
using Terminal.Gui;

namespace CarPark.ConsoleUI.ConsoleService
{
    public class CarsConsoleService
    {
        private readonly CarService _carService;

        public CarsConsoleService(CarService carService)
        {
            _carService = carService;
        }

        public void Add()
        {
            
        }


    }
}