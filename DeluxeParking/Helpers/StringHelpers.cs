using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking.Helpers
{
    internal static class StringHelpers
    {
        //
        internal static bool ValidateInput(this string? input)
        {
            //while (string.IsNullOrEmpty(input))
            //{
            //    if (input is "p")
            //    {
            //        Console.WriteLine("You chose to park vehicle");
            //    }
            //    else if (input is "c")
            //    {
            //        Console.WriteLine("You chose to check out vehicle");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Felaktig inmatning, försök igen.");
            //        input = "";
            //    }
            //}

            return input is "p" || input is "c";
        }

        internal static char GetRandomLetter()
        {
            Random random = new();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return letters[random.Next(letters.Length)];
        }

        internal static string AppendLetter(this String value)
        {
            Random random = new();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            value += letters[random.Next(letters.Length)];
            return value;
        }
    }
}
