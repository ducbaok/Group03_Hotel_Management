namespace HotelReservation
{
    public static partial class Event
    {
        public static Action<AccountVerificationResult>? OnAccountVerificated { get; set; }
    }
}
