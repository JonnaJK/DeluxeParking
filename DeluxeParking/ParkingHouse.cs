using DeluxeParking.Classes;
using DeluxeParking.Classes.Vehicles;
using DeluxeParking.Helpers;
using DeluxeParking.Interfaces;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace DeluxeParking
{
    internal class ParkingHouse
    {
        // Fundera om det är bättre att ha detta som en privat klass variabel eller om jag ska ha det som en inparameter i ParkingHouse konstruktorn???
        private readonly int _numberOfParkingSpots = 30;
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
                GUI.DrawParkingHouse(Parkingspots, VehiclesInQueue);

                Menu();

                Console.Clear();
            }
        }

        private void Menu()
        {
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
                    ManageParking();
                    break;
                case "c":
                    ManageCheckOut();
                    break;
                default:
                    break;
            }
        }

        // TODO: Fix all Console WriteLines to something in GUI or something else
        // TODO: Change functionname
        private void ManageParking()
        {
            var isSuccessfull = false;

            // Find parkingspot for vehicle, getvehicletype first to know where to find a spot
            if (VehiclesInQueue.Count > 0)
            {
                for (int i = 0; i < VehiclesInQueue.Count; i++)
                {
                    var vehicle = VehiclesInQueue[i];
                    isSuccessfull = TryToAssign(vehicle);
                    if (isSuccessfull)
                    {
                        Console.WriteLine($"You hace successfully parked the {vehicle.Type}, with registration number {vehicle.RegistrationNumber}.");
                        Console.WriteLine();
                        break;
                    }
                }
                if (!isSuccessfull)
                {
                    Console.WriteLine("Unfortunately there were no available parkingspots for the vehicles in queue");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("There is no vehicles in queue to park");
            }
        }

        private bool TryToAssign<T>(T vehicle)
        {
            // Fundera om det är bättre att skicka med bool som inparameter istället för en localvariabel som heter samma
            // Fundera även på om det är bättre att return true/false i de nästlade if, else if satserna istället för en till funktion som returnerar en bool, se exempel
            var partitionedParkingspots = Partition();
            var emptyParkingspotsNotForBus = partitionedParkingspots.FirstOrDefault(x => x.Count % 2 != 0 && x.TrueForAll(x => x.IsEmpty));
            var emptyParkingspots = Parkingspots.Where(x => x.IsEmpty).ToList();

            if (vehicle is Car car)
            {
                if (emptyParkingspotsNotForBus?.Count > 0)
                {
                    // Exempel från rad 95
                    //isSuccessfull = Assign(car, emptyParkingspotsNotForBus);
                    Assign(car, emptyParkingspotsNotForBus);
                    return true;
                }
                else if (emptyParkingspots?.Count > 0)
                {
                    Assign(car, emptyParkingspots);
                    return true;
                }
            }
            else if (vehicle is Motorcycle motorcycle)
            {
                var halfEmptyParkingspots = Parkingspots.Where(x => x.Size is 1).ToList();

                if (halfEmptyParkingspots.Count > 0)
                {
                    Assign(motorcycle, halfEmptyParkingspots);
                    return true;
                }
                else if (emptyParkingspotsNotForBus?.Count > 0)
                {
                    Assign(motorcycle, emptyParkingspotsNotForBus);
                    return true;
                }
                else if (emptyParkingspots?.Count > 0)
                {
                    Assign(motorcycle, emptyParkingspots);
                    return true;
                }
            }
            else if (vehicle is Bus bus && emptyParkingspots.Count > 1)
            {
                Assign(bus, emptyParkingspots);
                return true;
            }
            return false;
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

        // Ska jag ändra till ParkCar, ParkMotorcycle, ParkBus???? alternativt en icke generisk overload??
        private void Assign<T>(T vehicle, List<Parkingspot> emptyParkingspots)
        {
            if (vehicle is Car car)
            {
                emptyParkingspots[0].ParkedVehicles.Add(car);
                car.StartTime = DateTime.Now;
                emptyParkingspots[0].Size = 2;
                VehiclesInQueue.Remove(car);
            }
            else if (vehicle is Motorcycle motorcycle)
            {
                emptyParkingspots[0].ParkedVehicles.Add(motorcycle);
                motorcycle.StartTime = DateTime.Now;
                emptyParkingspots[0].Size++;
                VehiclesInQueue.Remove(motorcycle);
            }
            else if (vehicle is Bus bus)
            {
                for (int i = 0; i < emptyParkingspots.Count; i++)
                {
                    if (emptyParkingspots[i].ID == emptyParkingspots[i + 1].ID - 1)
                    {
                        emptyParkingspots[i].ParkedVehicles.Add(bus);
                        emptyParkingspots[i].Size = 2;

                        emptyParkingspots[i + 1].ParkedVehicles.Add(bus);
                        bus.StartTime = DateTime.Now;
                        emptyParkingspots[i + 1].Size = 2;
                        emptyParkingspots[i + 1].IsEmpty = false;
                        VehiclesInQueue.Remove(bus);
                        break;
                    }
                }
            }
            emptyParkingspots[0].IsEmpty = false;
        }

        private void ManageCheckOut()
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
                            CheckOutVehicle(car);
                            Parkingspots[i].ParkedVehicles.Remove(car);
                            Parkingspots[i].Size = 0;
                            Parkingspots[i].IsEmpty = true;
                            isSuccess = true;
                            break;
                        }
                        else if (Parkingspots[i].ParkedVehicles[j] is Motorcycle motorcycle)
                        {
                            CheckOutVehicle(motorcycle);
                            Parkingspots[i].ParkedVehicles.Remove(motorcycle);
                            Parkingspots[i].Size--;
                            if (Parkingspots[i].Size is 0)
                                Parkingspots[i].IsEmpty = true;
                            isSuccess = true;
                            break;
                        }
                        else if (Parkingspots[i].ParkedVehicles[j] is Bus bus)
                        {
                            CheckOutVehicle(bus);
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
        
        // TODO: Think about to make this one generic function or if it is better to have non-generic overload?
        private void CheckOutVehicle(Car car)
        {
            TimeSpan timeSpan = DateTimeOffset.Now - car.StartTime;
            var cost = timeSpan.Minutes * 1.5;
            Console.WriteLine($"Parkingprice for {car.RegistrationNumber} is {cost} SEK");
            Console.ReadKey();
        }

        private void CheckOutVehicle(Motorcycle motorcycle)
        {
            TimeSpan timeSpan = DateTimeOffset.Now - motorcycle.StartTime;
            var cost = timeSpan.Minutes * 1.5;
            Console.WriteLine($"Parkingprice for {motorcycle.RegistrationNumber} is {cost} SEK");
            Console.ReadKey();
        }

        private void CheckOutVehicle(Bus bus)
        {
            TimeSpan timeSpan = DateTimeOffset.Now - bus.StartTime;
            var cost = timeSpan.Minutes * 1.5;
            Console.WriteLine($"Parkingprice for {bus.RegistrationNumber} is {cost * 2} SEK");
            Console.ReadKey();
        }
    }
}
