using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking.Helpers
{
    internal class StringHelpers
    {
        internal static string ValidateInput()
        {
            string input = Console.ReadKey().ToString().ToLower();

            while (string.IsNullOrEmpty(input) || input is not "p" || input is not "c")
            {
                Console.WriteLine("Felaktig inmatning, försök igen.");
                input = Console.ReadKey().ToString().ToLower();
            }

            return input;
        }
    }
}
