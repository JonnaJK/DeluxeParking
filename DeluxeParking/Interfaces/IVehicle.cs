namespace DeluxeParking.Interfaces
{
    internal interface IVehicle
    {
        string Color { get; set; }
        string RegistrationNumber { get; set; }
        string Type { get; init; }
        string GetUniqueProperty();
    }
}