namespace DeluxeParking.Helpers
{
    internal static class StringHelpers
    {
        internal static bool ValidateInput(this string? input, string choice1, string choice2)
        {
            return input == choice1 || input == choice2;
        }

        internal static bool ValidateInput(this string? input, string choice1, string choice2, string choice3)
        {
            return input == choice1 || input == choice2 || input == choice3;
        }

        internal static string? GetAndValidateInput(this string? input, string validateChoice1, string validateChoice2, string? validateChoice3)
        {
            if (validateChoice3 is null)
            {
                while (!input.ValidateInput(validateChoice1, validateChoice2))
                {
                    input = GUI.GetInput("Wrong input, try again.")?.ToLower();
                }
            }
            else
            {
                while (!input.ValidateInput(validateChoice1, validateChoice2, validateChoice3))
                {
                    input = GUI.GetInput("Wrong input, try again.")?.ToLower();
                }
            }

            return input;
        }

        internal static string CheckAndRetryIfInvalid(string? input)
        {
            while (string.IsNullOrEmpty(input))
            {
                input = GUI.GetInput("No input, try again.");
            }
            return input;
        }

        internal static char GetRandomLetter()
        {
            Random random = new();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return letters[random.Next(letters.Length)];
        }
    }
}
