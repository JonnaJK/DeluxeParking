using DeluxeParking.Helpers;

namespace DeluxeParking.Classes.Vehicles
{
    internal class Motorcycle : VehicleBase
    {
        public string Brand { get; set; }

        public Motorcycle()
        {
            var brand = GUI.GetInput("What brand is the motorcycle?");
            brand = StringHelpers.CheckAndRetryIfInvalid(brand);
            Brand = brand;
        }

        public override string GetUniqueProperty()
        {
            return Brand;
        }
    }
}
