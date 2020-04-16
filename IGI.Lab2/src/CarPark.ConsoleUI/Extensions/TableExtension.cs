using System.Collections.Generic;
using ConsoleTables.Core;

namespace CarPark.ConsoleUI.Extensions
{
    public static class TableExtension
    {
        public static void ToTable<T>(this IEnumerable<T> items)
        {
            ConsoleTable.From(items).Write();
        }
    }
}