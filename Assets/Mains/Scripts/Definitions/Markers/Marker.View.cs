using System;

namespace YNL.Checkotel
{
    public delegate void OnSearchingResultRequested(string address, Room.RoomType room);
    public delegate void OnHotelInformationDisplayed(UID hotelID, bool isSearchingResult);
    public delegate void OnHotelFacilitiesDisplayed(UID hotelID);
    public delegate void OnHotelRoomsDisplayed(UID hotelID);
    public delegate void OnPaymentRequested(UID hotelID, UID roomID);

    public static partial class Marker
    {
        public static Action OnDatabaseSerializationDone { get; set; }
        public static Action OnRuntimeSavingRequested { get; set; }

        public static Action<ViewType, bool, bool> OnViewPageSwitched { get; set; }

        public static Action OnNotificationViewOpened { get; set; }
        public static Action<SuggestFilterType> OnSuggestFilterSelected { get; set; }

        public static Action<string> OnAddressSearchSubmitted { get; set; }

        public static OnSearchingResultRequested OnSearchingResultRequested { get; set; }
        public static OnHotelInformationDisplayed OnHotelInformationDisplayed{ get; set; }
        public static OnHotelFacilitiesDisplayed OnHotelFacilitiesDisplayed { get; set; }
        public static OnHotelRoomsDisplayed OnHotelRoomsDisplayed { get; set; }
        public static OnPaymentRequested OnPaymentRequested { get; set; }
    }
}