using System;

namespace YNL.Checkotel
{
    public delegate void OnHotelInformationDisplayed(UID hotelID, bool isSearchingResult);
    public delegate void OnSearchingResultRequested(string address, Room.StayType stay, Room.RoomType room, DateTime checInTime, byte duration);

    public static partial class Marker
    {
        public static Action<ViewType, bool, bool> OnViewPageSwitched { get; set; }

        public static Action OnNotificationViewOpened { get; set; }
        public static Action<SuggestFilterType> OnSuggestFilterSelected { get; set; }
        public static Action<DateTime, byte> OnTimeRangeChanged { get; set; }

        public static Action<string> OnAddressSearchSubmitted { get; set; }
        public static Action<DateTime, byte> OnTimeRangeSubmitted { get; set; }

        public static OnSearchingResultRequested OnSearchingResultRequested { get; set; }
        public static OnHotelInformationDisplayed OnHotelInformationDisplayed{ get; set; }
    }
}