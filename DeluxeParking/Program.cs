using DeluxeParking.Classes.Vehicles;
using DeluxeParking.Helpers;
using DeluxeParking.Interfaces;

namespace DeluxeParking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParkingHouse parkingHouse = new();
            parkingHouse.Run();
        }
    }
}