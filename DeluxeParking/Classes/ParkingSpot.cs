using DeluxeParking.Interfaces;

namespace DeluxeParking.Classes
{
    internal class Parkingspot
    {
        public int ID { get; set; }
        public int Size { get; set; }
        public bool IsEmpty { get; set; } = true;
        public List<IVehicle> ParkedVehicles { get; set; } = new();

        public Parkingspot(int id)
        {
            ID = id;
        }
    }
}
