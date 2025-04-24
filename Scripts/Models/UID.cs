namespace HotelReservation
{
    public struct UID
    {
        private uint _id;

        public UID(uint id)
        {
            _id = id;
        }

        public static implicit operator uint(UID id) => id._id;
        public static implicit operator UID(uint id) => new(id);
    }
}
