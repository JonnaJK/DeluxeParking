using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking.Helpers
{
    internal static class StringHelpers
    {
        internal static bool ValidateInput(this string? input, string choice1, string choice2)
        {
            return input == choice1 || input == choice2;
        }

        internal static char GetRandomLetter()
        {
            Random random = new();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return letters[random.Next(letters.Length)];
        }

        internal static string? ValidateAndGetCorrectInput(this string? input, string validateChoice1, string validateChoice2)
        {
            while (!input.ValidateInput(validateChoice1, validateChoice2))
            {
                input = GUI.GetInput("Wrong input, try again.")?.ToLower();
            }
            return input;
        }

        internal static string CheckAndReturnWhenIsNotNullOrEmpty(string? input)
        {
            while (string.IsNullOrEmpty(input))
            {
                input = GUI.GetInput("Null or empty input, try again.");
            }
            return input;
        }
    }
}
