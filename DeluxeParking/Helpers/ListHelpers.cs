using DeluxeParking.Classes;

namespace DeluxeParking.Helpers
{
    internal static class ListHelpers
    {
        public static List<List<Parkingspot>> Partition(List<Parkingspot> parkingspots)
        {
            List<List<Parkingspot>> partitioned = new();
            List<Parkingspot> bucket = new();
            var endedHere = false;
            var last = parkingspots[0];
            bucket.Add(last);
            for (int i = 1; i < parkingspots.Count; i++)
            {
                var current = parkingspots[i];
                if (last.Size == current.Size)
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
    }
}
