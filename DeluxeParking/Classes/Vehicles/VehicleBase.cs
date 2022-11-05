using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeParking.Helpers;
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
            RegistrationNumber = GetRegistrationNumber();
            Color = GetVehicleColorFromUser();
        }

        private static string GetVehicleColorFromUser()
        {
            var input = GUI.GetInput("What color does the vehicle have?");
            var color = StringHelpers.CheckAndReturnWhenIsNotNullOrEmpty(input);
            return color;
        }

        internal static string GetRegistrationNumber()
        {
            var registrationNumber = "";
            for (int i = 0; i < 3; i++)
            {
                registrationNumber += StringHelpers.GetRandomLetter();
            }
            for (int i = 0; i < 3; i++)
            {
                registrationNumber += IntHelpers.GetRandomNumber().ToString();
            }

            return registrationNumber;
        }
    }
}
