using DeluxeParking.Helpers;

namespace DeluxeParking.Classes.Vehicles
{
    internal class Bus : VehicleBase
    {
        public int NumberOfSeats { get; set; }

        public Bus()
        {
            // Kommentar till Micke:
            // Nedan rad hade jag endast för att slippa mata in NumberOfSeats för varje buss som skapas,
            // men jag glömde ta bort den inför redovisningen.
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
