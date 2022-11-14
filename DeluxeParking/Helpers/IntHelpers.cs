namespace DeluxeParking.Helpers
{
    internal class IntHelpers
    {
        internal static int GetRandomNumber()
        {
            Random random = new();
            List<int> numbers = new()
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9
            };

            return numbers[random.Next(numbers.Count)];
        }

        internal static int TryToParseInt(string? input)
        {
            int number;
            while (!int.TryParse(input, out number))
            {
                input = GUI.GetInput("Input have to be numbers, try again.");
            }
            if (number < 0)
            {
                Console.WriteLine("Seats can not be a negative number, therefore it has been converted to non-negative");
                number = Math.Abs(number);
            }
            return number;
        }
    }
}
