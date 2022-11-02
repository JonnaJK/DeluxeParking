namespace DeluxeParking.Interfaces
{
    internal interface IVehicleBase
    {
        string Color { get; set; }
        string RegistrationNumber { get; set; }
        string Type { get; init; }
    }
}