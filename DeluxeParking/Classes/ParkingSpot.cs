using DeluxeParking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking.Classes
{
    internal class ParkingSpot
    {
        public int Size { get; set; }
        public bool IsEmpty { get; set; } = true;
        public List<IVehicle> Vehicles { get; set; } = new();
    }
}
