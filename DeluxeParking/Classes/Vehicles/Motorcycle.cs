using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking.Classes.Vehicles
{
    internal class Motorcycle : VehicleBase
    {
        public string Brand { get; set; }

        public Motorcycle(string brand)
        {
            Brand = brand;
        }
    }
}
