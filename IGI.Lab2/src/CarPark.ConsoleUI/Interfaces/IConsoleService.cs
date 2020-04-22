
using System.Threading.Tasks;

namespace CarPark.ConsoleUI.Interfaces
{
    public interface IConsoleService
    {
        Task StartMenu();

        void PrintMenu();

        Task PrintItems();
    }
}