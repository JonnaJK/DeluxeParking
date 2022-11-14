using DeluxeParking.Helpers;

namespace DeluxeParking.Classes.Vehicles
{
    internal class Bus : VehicleBase
    {
        public int NumberOfSeats { get; set; }

        public Bus()
        {
            NumberOfSeats = 23;
            var input = GUI.GetInput("How many seats does the bus have?");
            int numberOfSeats = IntHelpers.TryToParseInt(input);
            NumberOfSeats = numberOfSeats;
        }

        public override string GetUniqueProperty()
        {
            return NumberOfSeats + " seats";
        }
    }
}
