using DeluxeParking.Classes.Vehicles;
using DeluxeParking.Interfaces;

namespace DeluxeParking.Classes
{
    internal class Parkingspot
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public List<IVehicle> ParkedVehicles { get; set; } = new();
        public DateTimeOffset StartTime { get; set; }

        public Parkingspot(int id)
        {
            Id = id;
        }

        internal static void CalculateCost(Parkingspot parkingspot, IVehicle parkedVehicle)
        {
            var multiplier = parkedVehicle is Bus ? 2 : 1;
            TimeSpan timeSpan = DateTimeOffset.Now - parkingspot.StartTime;
            var cost = timeSpan.Minutes * 1.5 * multiplier;
            Console.WriteLine(  $"Parkingprice for {parkedVehicle.RegistrationNumber} is {cost} SEK" +
                                $"\nPress enter to continue");
        }
    }
}
