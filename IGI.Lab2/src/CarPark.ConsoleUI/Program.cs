using System.IO;
using System.Threading.Tasks;
using CarPark.ConsoleUI.ConsoleService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarPark.ConsoleUI
{
    class Program
    {
        static async Task Main()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var services = DiConfig.Configure(config);

            var mainMenu = services.GetService<MenuConsoleService>();
            await mainMenu.StartMenu();
        }
    }
}
