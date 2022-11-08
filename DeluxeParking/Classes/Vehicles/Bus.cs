using DeluxeParking.Helpers;
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

        public Bus()
        {
            NumberOfSeats = 23;
            //var input = GUI.GetInput("How many seats does the bus have?");
            //int numberOfSeats = IntHelpers.TryToParseInt(input);
            //NumberOfSeats = numberOfSeats;
        }
    }
}
