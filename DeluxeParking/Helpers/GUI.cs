using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking.Helpers
{
    internal static class GUI
    {
        internal static string? GetInput(string message = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }
            return Console.ReadLine();
        }
    }
}
