namespace DeluxeParking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParkingHouse parkingHouse = new(15);
            parkingHouse.Run();
        }
    }
}