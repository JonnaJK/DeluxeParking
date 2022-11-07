using DeluxeParking.Classes;
using DeluxeParking.Classes.Vehicles;
using DeluxeParking.Helpers;
using DeluxeParking.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeluxeParking
{
    internal class ParkingHouse
    {
        // Attributes
        private readonly int _numberOfParkingSpots = 15;
        private readonly Random _random = new();

        // Properties
        public List<ParkingSpot> ParkingSpots { get; set; }
        public List<IVehicle> VehiclesInQueue { get; set; } = new();

        // Contructor
        public ParkingHouse()
        {
            ParkingSpots = new();
            for (int i = 0; i < _numberOfParkingSpots; i++)
            {
                ParkingSpots.Add(new ParkingSpot());
            }
        }

        // Functions
        public void Run()
        {
            while (true)
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

                // WriteAllValues
                WriteAllValues();
            }
        }

        private void ParkVehicle()
        {
            // Get random vehicle and add the unique properties from user to vehicle
            VehicleBase.GetRandomVehicle(_random, VehiclesInQueue);

            // Find parkingspot for vehicle, getvehicletype first to know where to find a spot
            var emptySpots = ParkingSpots.Where(x => x.IsEmpty).ToList();
            var halfEmptySpots = ParkingSpots.Where(x => x.Size is 1).ToList();
            for (int i = 0; i < VehiclesInQueue.Count; i++)
            {
                if (VehiclesInQueue[i] is Car car && emptySpots.Count > 0)
                {
                    emptySpots[0].Vehicles.Add(car);
                    emptySpots[0].Size += 2;
                    emptySpots[0].IsEmpty = false;
                    VehiclesInQueue.Remove(car);
                    break;
                }
                else if (VehiclesInQueue[i] is Motorcycle motorcycle)
                {
                    if (halfEmptySpots.Count > 0)
                    {
                        halfEmptySpots[0].Vehicles.Add(motorcycle);
                        halfEmptySpots[0].Size++;
                        halfEmptySpots[0].IsEmpty = false;
                        VehiclesInQueue.Remove(motorcycle);
                    }
                    else if (emptySpots.Count > 0)
                    {
                        emptySpots[0].Vehicles.Add(motorcycle);
                        emptySpots[0].Size++;
                        emptySpots[0].IsEmpty = false;
                        VehiclesInQueue.Remove(motorcycle);
                    }
                    break;
                }
                else if (VehiclesInQueue[i] is Bus bus && emptySpots.Count >= 2)
                {
                    emptySpots[0].Vehicles.Add(bus);
                    emptySpots[0].Size += 2;
                    emptySpots[0].IsEmpty = false;
                    emptySpots[1].Vehicles.Add(bus);
                    emptySpots[1].Size += 2;
                    emptySpots[1].IsEmpty = false;
                    VehiclesInQueue.Remove(bus);
                    break;
                }
            }
        }

        private void CheckOutVehicle()
        {
        }

        private static void CreateParkingSpots()
        {

        }

        private void WriteAllValues()
        {
            var index = 1;
            foreach (var parkingspot in ParkingSpots)
            {
                Console.Write($"Spot {index}:");
                if (!parkingspot.IsEmpty)
                {
                    for (int i = 0; i < parkingspot.Vehicles.Count; i++)
                    {
                        if (parkingspot.Vehicles[i] is Car car)
                        {
                            Console.Write("\t\tType: " + car.Type);
                            Console.Write("\tRegNr: " + car.RegistrationNumber);
                            Console.Write("\tColor: " + car.Color);
                            Console.Write("\tElectric: " + car.Electric);
                        }
                        else if (parkingspot.Vehicles[i] is Motorcycle motorcycle)
                        {
                            Console.Write("\t\tType: " + motorcycle.Type);
                            Console.Write("\tRegNr: " + motorcycle.RegistrationNumber);
                            Console.Write("\tColor: " + motorcycle.Color);
                            Console.Write("\tBrand: " + motorcycle.Brand);
                        }
                        else if (parkingspot.Vehicles[i] is Bus bus)
                        {
                            Console.Write("\t\tType: " + bus.Type);
                            Console.Write("\tRegNr: " + bus.RegistrationNumber);
                            Console.Write("\tColor: " + bus.Color);
                            Console.Write("\tNr of seats: " + bus.NumberOfSeats);
                        }
                        if (parkingspot.Vehicles.Count > 1)
                        {
                            Console.WriteLine();
                        }
                    }
                }
                Console.WriteLine();
                index++;
            }
            index = 1;
            if (VehiclesInQueue.Count > 0)
            {
                for (int i = 0; i < VehiclesInQueue.Count; i++)
                {
                    Console.WriteLine();
                    Console.Write($"Queue spot: {index}: ");
                    Console.Write(VehiclesInQueue[i].Type + "\t");
                    Console.Write(VehiclesInQueue[i].RegistrationNumber);
                    index++;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Queue is empty.");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}
