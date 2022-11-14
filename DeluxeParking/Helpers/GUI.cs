using DeluxeParking.Classes;
using DeluxeParking.Interfaces;

namespace DeluxeParking.Helpers
{
    internal static class GUI
    {
        internal static void DrawParkingHouse(List<Parkingspot> parkingspots, List<IVehicle> vehiclesInQueue)
        {
            for (int i = 0; i < parkingspots.Count; i++)
            {
                if (parkingspots[i].Size > 0)
                {
                    for (int j = 0; j < parkingspots[i].ParkedVehicles.Count; j++)
                    {
                        var vehicle = parkingspots[i].ParkedVehicles[j];
                        string parkingspotID = parkingspots[i].Id.ToString().PadLeft(2, '0') + ": ";
                        string message = vehicle.Type.PadRight(10);
                        message += "\tRegNr: " + vehicle.RegistrationNumber;
                        message += "\tColor: " + vehicle.Color;

                        message += "\t" + vehicle.GetUniqueProperty(); 

                        if (string.IsNullOrEmpty(message))
                            Console.WriteLine(parkingspots[i].Id);
                        else
                            Console.WriteLine(parkingspotID.PadRight(7) + message);
                    }
                }
                else
                {
                    Console.WriteLine(parkingspots[i].Id.ToString().PadLeft(2, '0') + ": ");
                }
            }
            var index = 1;
            if (vehiclesInQueue.Count > 0)
            {
                for (int i = 0; i < vehiclesInQueue.Count; i++)
                {
                    Console.Write($"\nQueue spot: {index}: ");
                    Console.Write(vehiclesInQueue[i].Type + "\t");
                    Console.Write(vehiclesInQueue[i].RegistrationNumber);
                    index++;
                }
            }
            else
            {
                Console.WriteLine("Queue is empty.");
            }
            Console.WriteLine();
        }

        internal static string? GetInput(string message = "")
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }
    }
}
