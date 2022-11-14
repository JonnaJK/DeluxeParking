using DeluxeParking.Helpers;

namespace DeluxeParking.Classes.Vehicles
{
    internal class Car : VehicleBase
    {
        public bool IsElectric { get; set; }

        public Car()
        {
            var input = GUI.GetInput("Is the car electric? Y/N")?.ToLower();
            input = StringHelpers.GetAndValidateInput(input, "y", "n", null);
            if (input is "y")
                IsElectric = true;
        }

        public override string GetUniqueProperty()
        {
            return IsElectric ? "Electric" : "Not electric";
        }
    }
}
