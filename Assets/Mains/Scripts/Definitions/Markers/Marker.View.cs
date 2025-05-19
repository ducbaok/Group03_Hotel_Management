using System;

namespace YNL.Checkotel
{
    public static partial class Marker
    {
        public static Action<ViewType, byte, bool, bool> OnViewPageSwitched { get; set; }

        public static Action OnNotificationViewOpened { get; set; }
        public static Action<SuggestFilterType> OnSuggestFilterSelected { get; set; }
        public static Action<DateTime, byte> OnTimeRangeChanged { get; set; }

        public static Action<string> OnAddressSearchSubmitted { get; set; }
        public static Action<DateTime, byte> OnTimeRangeSubmitted { get; set; }

        public static Action<string, Room.StayType, Room.RoomType, DateTime, byte> OnSearchingResultRequested { get; set; }
    }
}