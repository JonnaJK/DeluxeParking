using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeParking.Interfaces;

namespace DeluxeParking.Classes.Vehicles
{
    internal class VehicleBase : IVehicleBase
    {
        public string Type { get; init; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }

        public VehicleBase()
        {
            Type = this.GetType().Name;
        }
    }
}
