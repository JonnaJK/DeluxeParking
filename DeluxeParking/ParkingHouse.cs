using DeluxeParking.Classes;
using DeluxeParking.Classes.Vehicles;
using DeluxeParking.Helpers;
using DeluxeParking.Interfaces;
using System.Drawing;

namespace DeluxeParking
{
    internal class ParkingHouse
    {
        private readonly int _numberOfParkingSpots = 15;
        private readonly Random _random = new();

        public List<Parkingspot> Parkingspots { get; set; }
        public List<IVehicle> VehiclesInQueue { get; set; } = new();

        public ParkingHouse()
        {
            Parkingspots = new();
            for (int i = 1; i <= _numberOfParkingSpots; i++)
            {
                Parkingspots.Add(new Parkingspot(i));
            }
        }

        public void Run()
        {
            while (true)
            {
                // DrawParkingHouse();
                GUI.WriteValues(Parkingspots, VehiclesInQueue);

                // Menu()
                Console.WriteLine("Choose action:\n" +
                                    "[G] - Get new Vehicle in queue to parking\n" +
                                    "[P] - Park vehicle from queue\n" +
                                    "[C] - Check out existing vehicle\n");

                // Eventuellt göra detta till en egen funktion eftersom den återupprepas.
                var input = Console.ReadLine()?.ToLower();
                input = StringHelpers.ValidateAndGetCorrectInput(input, "g", "p", "c");

                switch (input)
                {
                    case "g":
                        VehicleBase.GetRandomVehicle(_random, VehiclesInQueue);
                        break;
                    case "p":
                        ParkVehicle();
                        break;
                    case "c":
                        CheckOutVehicle();
                        break;
                    default:
                        break;
                }
                Console.Clear();
            }
        }

        private void ParkVehicle()
        {
            var partitionedParkingspots = Partition();
            var emptySpotsNotForBus = partitionedParkingspots.FirstOrDefault(x => x.Count % 2 != 0 && x.TrueForAll(x => x.IsEmpty));
            //var emptyParkingspotForBus = partitionedParkingspots.FirstOrDefault(x => x.Count % 2 == 0 && x.TrueForAll(x => x.IsEmpty));
            var emptySpots = Parkingspots.Where(x => x.IsEmpty).ToList();
            var halfEmptySpots = Parkingspots.Where(x => x.Size is 1).ToList();
            var isSuccessfull = false;
            // Find parkingspot for vehicle, getvehicletype first to know where to find a spot
            if (VehiclesInQueue.Count is 0)
            {
                Console.WriteLine("There is no vehicles in queue to park");
            }
            else
            {
                for (int i = 0; i < VehiclesInQueue.Count; i++)
                {
                    var vehicle = VehiclesInQueue[i];
                    if (vehicle is Car car)
                    {
                        if (emptySpotsNotForBus?.Count > 0)
                        {
                            emptySpotsNotForBus[0].ParkedVehicles.Add(car);
                            emptySpotsNotForBus[0].Size = 2;
                            emptySpotsNotForBus[0].IsEmpty = false;
                            VehiclesInQueue.Remove(car);
                            isSuccessfull = true;
                        }
                        else if (emptySpots?.Count > 0)
                        {
                            emptySpots[0].ParkedVehicles.Add(car);
                            emptySpots[0].Size = 2;
                            emptySpots[0].IsEmpty = false;
                            VehiclesInQueue.Remove(car);
                            isSuccessfull = true;
                        }
                    }
                    else if (vehicle is Motorcycle motorcycle)
                    {
                        if (halfEmptySpots.Count > 0)
                        {
                            halfEmptySpots[0].ParkedVehicles.Add(motorcycle);
                            halfEmptySpots[0].Size++;
                            halfEmptySpots[0].IsEmpty = false;
                            VehiclesInQueue.Remove(motorcycle);
                            isSuccessfull = true;
                        }
                        else if (emptySpotsNotForBus?.Count > 0)
                        {
                            emptySpotsNotForBus[0].ParkedVehicles.Add(motorcycle);
                            emptySpotsNotForBus[0].Size++;
                            emptySpotsNotForBus[0].IsEmpty = false;
                            VehiclesInQueue.Remove(motorcycle);
                            isSuccessfull = true;
                        }
                        else if (emptySpots?.Count > 0)
                        {
                            emptySpots[0].ParkedVehicles.Add(motorcycle);
                            emptySpots[0].Size = 2;
                            emptySpots[0].IsEmpty = false;
                            VehiclesInQueue.Remove(motorcycle);
                            isSuccessfull = true;
                        }
                    }
                    else if (vehicle is Bus bus && emptySpots?.Count >= 2)
                    {
                        for (int j = 0; j < emptySpots.Count; j++)
                        {
                            if (emptySpots[j].ID == emptySpots[j + 1].ID - 1)
                            {
                                emptySpots[j].ParkedVehicles.Add(bus);
                                emptySpots[j].Size = 2;
                                emptySpots[j].IsEmpty = false;
                                emptySpots[j + 1].ParkedVehicles.Add(bus);
                                emptySpots[j + 1].Size = 2;
                                emptySpots[j + 1].IsEmpty = false;
                                VehiclesInQueue.Remove(bus);
                                isSuccessfull = true;
                                break;
                            }
                        }
                    }
                    if (isSuccessfull)
                    {
                        Console.WriteLine($"You hace successfully parked the {vehicle.Type}, with registration number {vehicle.RegistrationNumber}.");
                        break;
                    }
                }
                if (!isSuccessfull)
                    Console.WriteLine("Unfortunately there were no available parkingspots for the vehicles in queue");
            }
        }

        private List<List<Parkingspot>> Partition()
        {
            List<List<Parkingspot>> partitioned = new();
            List<Parkingspot> bucket = new();
            var endedHere = false;
            var last = Parkingspots[0];
            bucket.Add(last);
            for (int i = 1; i < Parkingspots.Count; i++)
            {
                var current = Parkingspots[i];
                if (last.IsEmpty == current.IsEmpty)
                {
                    bucket.Add(current);
                    last = current;
                    endedHere = true;
                }
                else
                {
                    partitioned.Add(bucket);
                    last = current;
                    bucket = new();
                    bucket.Add(last);
                    endedHere = true;
                }
            }
            if (endedHere)
            {
                partitioned.Add(bucket);
            }
            return partitioned;
        }

        private void CheckOutVehicle()
        {
            var input = GUI.GetInput("Enter registrationnumber to check out")?.ToUpper();
            input = StringHelpers.CheckAndReturnWhenIsNotNullOrEmpty(input);


            // TODO: Dela upp i tre olika metoder beroende på Vehicle type.
            var test = Parkingspots.Select(x => x.ParkedVehicles.FirstOrDefault(y => y.RegistrationNumber == input));

            var isSuccess = false;
            for (int i = 0; i < Parkingspots.Count; i++)
            {
                for (int j = 0; j < Parkingspots[i].ParkedVehicles.Count; j++)
                {
                    if (Parkingspots[i].ParkedVehicles[j].RegistrationNumber == input)
                    {
                        if (Parkingspots[i].ParkedVehicles[j] is Car car)
                        {
                            Parkingspots[i].ParkedVehicles.Remove(car);
                            Parkingspots[i].Size = 0;
                            Parkingspots[i].IsEmpty = true;
                            isSuccess = true;
                            break;
                        }
                        else if (Parkingspots[i].ParkedVehicles[j] is Motorcycle motorcycle)
                        {
                            Parkingspots[i].ParkedVehicles.Remove(motorcycle);
                            Parkingspots[i].Size--;
                            if (Parkingspots[i].Size is 0)
                                Parkingspots[i].IsEmpty = true;
                            isSuccess = true;
                            break;
                        }
                        else if (Parkingspots[i].ParkedVehicles[j] is Bus bus)
                        {
                            // TODO: Does not handle if [i] is the last parkingspot...
                            Parkingspots[i].ParkedVehicles.Remove(bus);
                            Parkingspots[i].IsEmpty = true;
                            Parkingspots[i].Size = 0;
                            Parkingspots[i + 1].ParkedVehicles.Remove(bus);
                            Parkingspots[i + 1].IsEmpty = true;
                            Parkingspots[i + 1].Size = 0;
                            isSuccess = true;
                            break;
                        }
                    }
                }
                if (isSuccess)
                {
                    break;
                }
            }
        }

        // How I had it first.
        private void WriteAllValues2()
        {
            var index = 1;
            foreach (var parkingspot in Parkingspots)
            {
                Console.Write($"Spot {index}:");
                if (!parkingspot.IsEmpty)
                {
                    for (int i = 0; i < parkingspot.ParkedVehicles.Count; i++)
                    {
                        if (parkingspot.ParkedVehicles[i] is Car car)
                        {
                            Console.Write("\t\tType: " + car.Type);
                            Console.Write("\tRegNr: " + car.RegistrationNumber);
                            Console.Write("\tColor: " + car.Color);
                            Console.Write("\tElectric: " + car.Electric);
                        }
                        else if (parkingspot.ParkedVehicles[i] is Motorcycle motorcycle)
                        {
                            Console.Write("\t\tType: " + motorcycle.Type);
                            Console.Write("\tRegNr: " + motorcycle.RegistrationNumber);
                            Console.Write("\tColor: " + motorcycle.Color);
                            Console.Write("\tBrand: " + motorcycle.Brand);
                        }
                        else if (parkingspot.ParkedVehicles[i] is Bus bus)
                        {
                            Console.Write("\t\tType: " + bus.Type);
                            Console.Write("\tRegNr: " + bus.RegistrationNumber);
                            Console.Write("\tColor: " + bus.Color);
                            Console.Write("\tNr of seats: " + bus.NumberOfSeats);
                        }
                        if (parkingspot.ParkedVehicles.Count > 1)
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
