using DeluxeParking.Classes;
using DeluxeParking.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParking
{
    internal class ParkingHouse
    {
        // Attributes
        private readonly int _numberOfSpots = 15;
        private readonly Random _random = new();

        // Properties
        public Dictionary<string, ParkingSpot> ParkingSpots { get; init; }

        public ParkingHouse()
        {
            ParkingSpots = new(_numberOfSpots);
        }

        public void Run()
        {
            // Menu()
            Console.WriteLine("Choose action:");
            Console.WriteLine("[P] - Park new vehicle");
            Console.WriteLine("[C] - Check out existing vehicle");
            var menuChoice = StringHelpers.ValidateInput();

            switch (menuChoice)
            {
                case "p":
                    ParkVehicle();
                    break;
                case "c":
                    CheckOutVehicle();
                    break;
                default:
                    break;
            }
        }

        private void ParkVehicle()
        {
            // Get random vehicle

        }

        private void CheckOutVehicle()
        {
            throw new NotImplementedException();
        }
    }
}
