namespace HotelReservation
{
    public class HotelManager
    {
        public Dictionary<UID, Hotel> Hotels = new();

        public async Task<List<UID>> SearchHotelAsync(List<UID> hotels, string query)
        {
            Func<List<UID>> task = () =>
                Hotels.Values.ToList().Where(h =>
                       h.Description.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || h.Description.Address.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(h => h.ID)
                .ToList();

            return await Task.Run(task);
        }
    }
}
