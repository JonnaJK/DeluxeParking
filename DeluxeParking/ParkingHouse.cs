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
        private readonly int _numberOfParkingSpots = 15;
        private readonly Random _random = new();

        // Properties
        public Dictionary<string, ParkingSpot> ParkingSpots { get; init; }
        public List<VehicleBase> VehiclesInQueue { get; set; } = new();

        // Contructor
        public ParkingHouse()
        {
            ParkingSpots = new(_numberOfParkingSpots);
        }

        // Functions
        public void Run()
        {
            // Menu()
            Console.WriteLine("Choose action:");
            Console.WriteLine("[P] - Park new vehicle");
            Console.WriteLine("[C] - Check out existing vehicle");

            var input = Console.ReadLine()?.ToLower();
            while (!input.ValidateInput())
            {
                input = GUI.GetInput("Wrong input, try again.")?.ToLower();
            }

            switch (input)
            {
                case "p":
                    "You chose to park vehicle".OutgoingMessage();
                    ParkVehicle();
                    break;
                case "c":
                    GUI.OutgoingMessage("You chose to check out vehicle.");
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
                var answer = GUI.GetInput("Is the car electric? Y/N");
                
                VehiclesInQueue.Add(new Car(_random));
            }
            else if (randomVehicle is 2)
            {
                //VehiclesInQueue.Add(new Motorcycle());
            }
            else
            {
                VehiclesInQueue.Add(new Bus());
            }

            // Check type in VehiclesInQueue
        }

        private void CheckOutVehicle()
        {
        }
    }
}
