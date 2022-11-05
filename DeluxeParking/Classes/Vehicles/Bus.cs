using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking.Classes.Vehicles
{
    internal class Bus : VehicleBase
    {
        public int NumberOfSeats { get; set; }

        public Bus(int numberOfSeats)
        {
            NumberOfSeats = numberOfSeats;
        }
    }
}
