using DeluxeParking.Helpers;
using DeluxeParking.Interfaces;

namespace DeluxeParking.Classes.Vehicles
{
    internal abstract class VehicleBase : IVehicle
    {
        public string Type { get; init; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public abstract string GetUniqueProperty();

        public VehicleBase()
        {
            Type = this.GetType().Name;
            RegistrationNumber = GetRegistrationNumber();
            Color = GetVehicleColorFromUser();
        }

        internal static void GetRandomVehicle(Random random, List<IVehicle> vehicles)
        {
            IVehicle vehicle = random.Next(3) switch
            {
                0 => new Car(),
                1 => new Motorcycle(),
                2 => new Bus(),
                _ => throw new NotImplementedException("HOW!"),
            };
            vehicles.Add(vehicle);
        }

        private static string GetVehicleColorFromUser()
        {
            var input = GUI.GetInput("What color does the vehicle have?");
            var color = StringHelpers.CheckAndRetryIfInvalid(input);
            return color;
        }

        internal static string GetRegistrationNumber()
        {
            var registrationNumber = "";
            for (int i = 0; i < 3; i++)
            {
                registrationNumber += StringHelpers.GetRandomLetter();
            }
            for (int i = 0; i < 3; i++)
            {
                registrationNumber += IntHelpers.GetRandomNumber().ToString();
            }

            return registrationNumber;
        }
    }
}
