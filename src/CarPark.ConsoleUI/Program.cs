using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Terminal.Gui;

namespace CarPark.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var services = DIConfig.Configure(config);

            Application.Init();
            var top = Application.Top;


            var win = new Window("CarPark")
            {
                X = 0,
                Y = 1,


                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(win);


            var menu = new MenuBar(new[]
            {
                new MenuBarItem("_Accidents", new[]
                {
                    new MenuItem("_Add", "", null),
                    new MenuItem("_Edit", "", null),
                    new MenuItem("_Remove", "", null)
                }),
                new MenuBarItem("_Cars", new[]
                {
                    new MenuItem("_Add", "", null),
                    new MenuItem("Remove", "", null),
                    new MenuItem("Edit", "", null)
                }),
                new MenuBarItem("_Contracts", new[]
                {
                    new MenuItem("_Add", "", null),
                    new MenuItem("Remove", "", null),
                    new MenuItem("Edit", "", null)
                }),
                new MenuBarItem("_CarModel", new[]
                {
                    new MenuItem("_Add", "", null),
                    new MenuItem("Remove", "", null),
                    new MenuItem("Edit", "", null)
                })
            });
            top.Add(menu);
            
            Application.Run();
        }
    }

}
