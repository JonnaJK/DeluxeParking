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
            var input = "";
            while (string.IsNullOrEmpty(input))
            {
                input = Console.ReadLine().ToLower();
                if (input is "p")
                {
                    Console.WriteLine("You chose to park vehicle");
                }
                else if (input is "c")
                {
                    Console.WriteLine("You chose to check out vehicle");
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning, försök igen.");
                    input = "";
                }
            }

            return input;
        }

        internal static string GetLetter()
        {
            Random random = new();
            List<char> letters = new()
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };

            return letters[random.Next(letters.Count)].ToString(); ;
        }
    }
}
