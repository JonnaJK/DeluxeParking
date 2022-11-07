using DeluxeParking.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking.Classes.Vehicles
{
    internal class Car : VehicleBase
    {
        public bool Electric { get; set; }

        public Car()
        {
            var input = GUI.GetInput("Is the car electric? Y/N");
            input = StringHelpers.ValidateAndGetCorrectInput(input, "y", "n");
            if (input is "y")
                Electric = true;
        }
    }
}
