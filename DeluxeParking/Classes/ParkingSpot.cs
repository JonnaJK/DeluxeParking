using DeluxeParking.Interfaces;

namespace DeluxeParking.Classes
{
    internal class ParkingSpot
    {
        public int Size { get; set; }
        public bool IsEmpty { get; set; } = true;
        public List<IVehicle> ParkedVehicles { get; set; } = new();
    }
}
