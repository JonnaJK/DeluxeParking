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

            // Eventuellt göra detta till en egen funktion eftersom den återupprepas.
            var input = Console.ReadLine()?.ToLower();
            input = StringHelpers.ValidateAndGetCorrectInput(input, "p", "c");

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
            // Get random vehicle and add the unique properties from user to vehicle
            var randomVehicle = 1; //_random.Next(1, 3);
            // Istället för nedan, ha en enum STATE??
            if (randomVehicle is 1)
            {
                "The vehicle to park is a car".OutgoingMessage();
                var isElectric = false;

                var input = GUI.GetInput("Is the car electric? Y/N");
                input = StringHelpers.ValidateAndGetCorrectInput(input, "y", "n");
                if (input is "y")
                    isElectric = true;

                VehiclesInQueue.Add(new Car(isElectric));
            }
            else if (randomVehicle is 2)
            {
                "The vehicle to park is a motorcycle".OutgoingMessage();

                var brand = GUI.GetInput("Of what brand is the motorcycle?");
                brand = StringHelpers.CheckAndReturnWhenIsNotNullOrEmpty(brand);

                VehiclesInQueue.Add(new Motorcycle(brand));
            }
            else
            {
                "The vehicle to park is a bus".OutgoingMessage();

                var input = GUI.GetInput("How many seats does the bus have?");
                int numberOfSeats = IntHelpers.TryToParseInt(input);

                VehiclesInQueue.Add(new Bus(numberOfSeats));
            }

            // Check type in VehiclesInQueue
        }

        private void CheckOutVehicle()
        {
        }
    }
}
