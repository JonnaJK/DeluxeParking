using DeluxeParking.Classes;
using DeluxeParking.Classes.Vehicles;
using DeluxeParking.Helpers;
using DeluxeParking.Interfaces;

namespace DeluxeParking
{
    internal class ParkingHouse
    {
        private readonly int _numberOfParkingSpots = 15;
        private readonly Random _random = new();

        public List<ParkingSpot> ParkingSpots { get; set; }
        public List<IVehicle> VehiclesInQueue { get; set; } = new();

        public ParkingHouse()
        {
            ParkingSpots = new();
            for (int i = 1; i <= _numberOfParkingSpots; i++)
            {
                ParkingSpots.Add(new ParkingSpot(i));
            }
        }

        public void Run()
        {
            while (true)
            {
                WriteAllValues();
                //DrawParkingHouse();

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
                Console.ReadKey();
                Console.Clear();
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
                    emptySpots[0].ParkedVehicles.Add(car);
                    emptySpots[0].Size += 2;
                    emptySpots[0].IsEmpty = false;
                    VehiclesInQueue.Remove(car);
                    break;
                }
                else if (VehiclesInQueue[i] is Motorcycle motorcycle)
                {
                    if (halfEmptySpots.Count > 0)
                    {
                        halfEmptySpots[0].ParkedVehicles.Add(motorcycle);
                        halfEmptySpots[0].Size++;
                        halfEmptySpots[0].IsEmpty = false;
                        VehiclesInQueue.Remove(motorcycle);
                    }
                    else if (emptySpots.Count > 0)
                    {
                        emptySpots[0].ParkedVehicles.Add(motorcycle);
                        emptySpots[0].Size++;
                        emptySpots[0].IsEmpty = false;
                        VehiclesInQueue.Remove(motorcycle);
                    }
                    break;
                }
                else if (VehiclesInQueue[i] is Bus bus && emptySpots.Count >= 2)
                {
                    emptySpots[0].ParkedVehicles.Add(bus);
                    emptySpots[0].Size += 2;
                    emptySpots[0].IsEmpty = false;
                    emptySpots[1].ParkedVehicles.Add(bus);
                    emptySpots[1].Size += 2;
                    emptySpots[1].IsEmpty = false;
                    VehiclesInQueue.Remove(bus);
                    break;
                }
            }
        }

        private void CheckOutVehicle()
        {
            var input = GUI.GetInput("Enter registrationnumber to check out")?.ToUpper();
            input = StringHelpers.CheckAndReturnWhenIsNotNullOrEmpty(input);


            // TODO: Dela upp i tre olika metoder beroende på Vehicle type.
            // TODO: Partition function/logic to look for gaps of 2, to not place motorcycle or car in.
            var test = ParkingSpots.Select(x => x.ParkedVehicles.FirstOrDefault(y => y.RegistrationNumber == input));

            var isSuccess = false;
            for (int i = 0; i < ParkingSpots.Count; i++)
            {
                for (int j = 0; j < ParkingSpots[i].ParkedVehicles.Count; j++)
                {
                    if (ParkingSpots[i].ParkedVehicles[j].RegistrationNumber == input)
                    {
                        if (ParkingSpots[i].ParkedVehicles[j] is Car car)
                        {
                            ParkingSpots[i].ParkedVehicles.Remove(car);
                            ParkingSpots[i].Size = 0;
                            ParkingSpots[i].IsEmpty = true;
                            isSuccess = true;
                            break;
                        }
                        else if (ParkingSpots[i].ParkedVehicles[j] is Motorcycle motorcycle)
                        {
                            ParkingSpots[i].ParkedVehicles.Remove(motorcycle);
                            ParkingSpots[i].Size--;
                            if (ParkingSpots[i].Size is 0)
                                ParkingSpots[i].IsEmpty = true;
                            isSuccess = true;
                            break;
                        }
                        else if (ParkingSpots[i].ParkedVehicles[j] is Bus bus)
                        {
                            // TODO: Does not handle if [i] is the last parkingspot...
                            ParkingSpots[i].ParkedVehicles.Remove(bus);
                            ParkingSpots[i].IsEmpty = true;
                            ParkingSpots[i].Size = 0;
                            ParkingSpots[i + 1].ParkedVehicles.Remove(bus);
                            ParkingSpots[i + 1].IsEmpty = true;
                            ParkingSpots[i + 1].Size = 0;
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
            foreach (var parkingspot in ParkingSpots)
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

        private void WriteAllValues()
        {
            // TODO: change to for instead of foreach
            foreach (var parkingspot in ParkingSpots)
            {
                if (!parkingspot.IsEmpty)
                {
                    for (int i = 0; i < parkingspot.ParkedVehicles.Count; i++)
                    {
                        var vehicle = parkingspot.ParkedVehicles[i];
                        string parkingspots = parkingspot.ID.ToString().PadLeft(2, '0') + ": ";
                        string message = vehicle.Type.PadRight(10);
                        message += "\tRegNr: " + vehicle.RegistrationNumber;
                        message += "\tColor: " + vehicle.Color;
                        if (vehicle is Car car)
                        {
                            message += "\t" + (car.Electric ? "Electric" : "Not electric");
                        }
                        else if (vehicle is Motorcycle motorcycle)
                        {
                            message += "\t" + motorcycle.Brand;
                        }
                        else if (vehicle is Bus bus)
                        {
                            //parkingspots = $"{index}-{index + 1}:";
                            message += "\t" + bus.NumberOfSeats + " seats";
                            //index++;
                        }
                        Console.WriteLine(parkingspots.PadRight(7) + message);
                    }
                }
            }
            var index = 1;
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
        }
    }
}
