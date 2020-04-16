using System.IO;
using CarPark.ConsoleUI.ConsoleService.MenuService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarPark.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var services = DiConfig.Configure(config);

            var mainMenu = services.GetService<MenuConsoleService>();
            mainMenu.ConsoleMenu();
        }
    }
}
