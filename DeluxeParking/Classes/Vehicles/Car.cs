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

        public Car(Random random)
        {
            //int probability = random.Next(101);
            //if (probability > 75)
            //{
            //    Electric = true;
            //}
        }
    }
}
