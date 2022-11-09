using DeluxeParking.Interfaces;

namespace DeluxeParking.Classes
{
    internal class ParkingSpot
    {
        public int ID { get; set; }
        public int Size { get; set; }
        public bool IsEmpty { get; set; } = true;
        public List<IVehicle> ParkedVehicles { get; set; } = new();

        public ParkingSpot(int id)
        {
            ID = id;
        }
    }
}
