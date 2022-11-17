using DeluxeParking.Classes;
using DeluxeParking.Classes.Vehicles;
using DeluxeParking.Helpers;
using DeluxeParking.Interfaces;

namespace DeluxeParking
{
    internal class ParkingHouse
    {
        private readonly Random _random = new();

        public List<Parkingspot> Parkingspots { get; set; }
        public List<IVehicle> VehiclesInQueue { get; set; } = new();

        public ParkingHouse(int numberOfParkingspots)
        {
            Parkingspots = new();
            for (int i = 1; i <= numberOfParkingspots; i++)
            {
                Parkingspots.Add(new Parkingspot(i));
            }
        }

        public void Run()
        {
            while (true)
            {
                GUI.DrawParkingHouse(Parkingspots, VehiclesInQueue);

                Menu();

                Console.ReadKey();
                Console.Clear();
            }
        }

        #region Done
        private void Menu()
        {
            Console.WriteLine("Choose action:\n" +
                                "[G] - Get new Vehicle in queue to parking\n" +
                                "[P] - Park vehicle from queue\n" +
                                "[C] - Check out existing vehicle\n");

            var input = Console.ReadLine()?.ToLower();
            input = StringHelpers.GetAndValidateInput(input, "g", "p", "c");

            switch (input)
            {
                case "g":
                    VehicleBase.GetRandomVehicle(_random, VehiclesInQueue);
                    break;
                case "p":
                    HandleVehicleQueue();
                    break;
                case "c":
                    ManageCheckOut();
                    break;
                default:
                    break;
            }
        }

        private void HandleVehicleQueue()
        {
            if (VehiclesInQueue.Count == 0)
            {
                Console.WriteLine(  "There are no vehicles in the queue." +
                                    "\nPress enter to continue");
                return;
            }

            var isSuccessfull = false;
            for (int i = 0; i < VehiclesInQueue.Count; i++)
            {
                var vehicle = VehiclesInQueue[i];
                isSuccessfull = TryToAssignVehicleToParking(vehicle);
                if (isSuccessfull)
                {
                    Console.WriteLine(  $"You have successfully parked the {vehicle.Type}, {vehicle.RegistrationNumber}." +
                                        $"\nPress enter to continue");
                    break;
                }
            }
            if (!isSuccessfull)
            {
                Console.WriteLine(  "No suitable parkingspots for the vehicles in the queue." +
                                    "\nPress enter to continue");
            }
        }

        private bool TryToAssignVehicleToParking(IVehicle vehicle)
        {
            var partitionedParkingspots = ListHelpers.Partition(Parkingspots);

            if (vehicle is Car car)
            {
                return HandleCarAssignment(car, partitionedParkingspots);
            }
            else if (vehicle is Motorcycle motorcycle)
            {
                return HandleMotorcycleAssignment(motorcycle, partitionedParkingspots);

            }
            else if (vehicle is Bus bus)
            {
                return HandleBusAssignment(bus);
            }
            return false;
        }

        private bool HandleCarAssignment(Car car, List<List<Parkingspot>> partitionedParkingspots)
        {
            var primaryParkingspots = partitionedParkingspots.FirstOrDefault(x => x.Count % 2 != 0 && x.TrueForAll(x => x.Size == 0));
            var secondaryParkingspots = Parkingspots.Where(x => x.Size == 0).ToList();

            if (primaryParkingspots?.Count > 0)
            {
                AssignCarToParkingspot(car, primaryParkingspots);
                return true;
            }
            else if (secondaryParkingspots?.Count > 0)
            {
                AssignCarToParkingspot(car, secondaryParkingspots);
                return true;
            }
            return false;
        }

        private bool HandleMotorcycleAssignment(Motorcycle motorcycle, List<List<Parkingspot>> partitionedParkingspots)
        {
            var primaryParkingspots = Parkingspots.Where(x => x.Size == 1).ToList();
            var secondaryParkingspots = partitionedParkingspots.FirstOrDefault(x => x.Count % 2 != 0 && x.TrueForAll(x => x.Size == 0));
            var lastResortParkingspots = Parkingspots.Where(x => x.Size == 0).ToList();

            if (primaryParkingspots.Count > 0)
            {
                AssignMotorcycleToParkingspot(motorcycle, primaryParkingspots);
                return true;
            }
            else if (secondaryParkingspots?.Count > 0)
            {
                AssignMotorcycleToParkingspot(motorcycle, secondaryParkingspots);
                return true;
            }
            else if (lastResortParkingspots?.Count > 0)
            {
                AssignMotorcycleToParkingspot(motorcycle, lastResortParkingspots);
                return true;
            }
            return false;
        }

        private bool HandleBusAssignment(Bus bus)
        {
            var emptyParkingspots = Parkingspots.Where(x => x.Size == 0).ToList();
            if (emptyParkingspots.Count > 1)
            {
                for (int i = 0; i < emptyParkingspots.Count; i++)
                {
                    if (emptyParkingspots[i].Id == emptyParkingspots[i + 1].Id - 1)
                    {
                        AssignBusToParkingspot(bus, emptyParkingspots, i);
                        break;
                    }
                }
                return true;
            }
            else
                return false;
        }

        private void AssignCarToParkingspot(Car car, List<Parkingspot> emptyParkingspots)
        {
            emptyParkingspots[0].StartTime = DateTime.Now;
            emptyParkingspots[0].ParkedVehicles.Add(car);
            emptyParkingspots[0].Size = 2;
            VehiclesInQueue.Remove(car);
        }

        private void AssignMotorcycleToParkingspot(Motorcycle motorcycle, List<Parkingspot> matchingParkingspots)
        {
            matchingParkingspots[0].StartTime = DateTime.Now;
            matchingParkingspots[0].ParkedVehicles.Add(motorcycle);
            matchingParkingspots[0].Size++;
            VehiclesInQueue.Remove(motorcycle);
        }

        private void AssignBusToParkingspot(Bus bus, List<Parkingspot> emptyParkingspots, int index)
        {
            emptyParkingspots[index].StartTime = DateTime.Now;
            emptyParkingspots[index].ParkedVehicles.Add(bus);
            emptyParkingspots[index].Size = 2;
            emptyParkingspots[index + 1].ParkedVehicles.Add(bus);
            emptyParkingspots[index + 1].Size = 2;
            VehiclesInQueue.Remove(bus);
        }

        private void ManageCheckOut()
        {
            var registrationNumber = GUI.GetInput("Enter registrationnumber to check out")?.Trim().ToUpper();
            registrationNumber = StringHelpers.CheckAndRetryIfInvalid(registrationNumber).ToUpper();

            // Kommentar till Micke:
            // Jag började med att först hämta vehicleToCheckOut av datatypen IVehicle.
            // Sedan ändrade jag om det så att jag först hämtade vilken Parkingspot bilen som ska checkas ut står på
            // Därav står variabelnamnet fortfarande som vehicleToChekOut fast det egentligen är en parkingspot som du kan se
            // när jag kallar på metoden CheckOut.
            var vehicleToCheckOut = Parkingspots
                .FirstOrDefault(x => x.ParkedVehicles.Any(y => y.RegistrationNumber == registrationNumber));

            if (vehicleToCheckOut is not null)
                CheckOut(vehicleToCheckOut, registrationNumber);
            else
                Console.WriteLine(  $"No vehicle with registrationnumber {registrationNumber} could be found" +
                                    $"\nPress enter to continue");
        }

        private void CheckOut(Parkingspot parkingspot, string registrationNumber)
        {
            var parkedVehicle = parkingspot.ParkedVehicles.First(x => x.RegistrationNumber == registrationNumber);
            parkingspot.ParkedVehicles.Remove(parkedVehicle);
            Parkingspot.CalculateCost(parkingspot, parkedVehicle);

            if (parkedVehicle is Car car)
            {
                parkingspot.Size = 0;
            }
            else if (parkedVehicle is Motorcycle motorcycle)
            {
                parkingspot.Size--;
            }
            else if (parkedVehicle is Bus bus)
            {
                parkingspot.Size = 0;
                Parkingspots[parkingspot.Id].ParkedVehicles.Remove(bus);
                Parkingspots[parkingspot.Id].Size = 0;
            }
        }
        #endregion Done
    }
}
