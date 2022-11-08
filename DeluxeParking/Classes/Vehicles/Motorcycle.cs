using DeluxeParking.Helpers;
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

        public Motorcycle()
        {
            Brand = "Yamaha";
            //var brand = GUI.GetInput("What brand is the motorcycle?");
            //brand = StringHelpers.CheckAndReturnWhenIsNotNullOrEmpty(brand);
            //Brand = brand;
        }
    }
}
