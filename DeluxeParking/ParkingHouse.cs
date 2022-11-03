using DeluxeParking.Classes;
using DeluxeParking.Classes.Vehicles;
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
        public List<VehicleBase> Vehicles { get; set; } = new();

        // Contructor
        public ParkingHouse()
        {
            ParkingSpots = new(_numberOfSpots);
        }

        // Functions
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
            var randomVehicle = _random.Next(1, 3);
            if (randomVehicle is 1)
            {
                Vehicles.Add(new Car());
            }
            else if (randomVehicle is 2)
            {

            }
            else
            {

            }
        }

        private void CheckOutVehicle()
        {
        }
    }
}
