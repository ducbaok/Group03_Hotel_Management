using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class InformationViewRoomPageUI : ViewPageUI
    {
        private VisualElement _labelField;
        private VisualElement _timeField;
        private VisualElement _timeIcon;
        private Label _timeText;
        private ListView _itemList;

        private List<RoomUnit> _rooms = new();
        private UID _hotelID;
        private Room.StayType _stayType;
        private DateTime _checkInTime;
        private byte _duration;

        protected override void VirtualAwake()
        {
            Marker.OnHotelRoomsDisplayed += OnHotelRoomsDisplayed;
        }

        private void OnDestroy()
        {
            Marker.OnHotelRoomsDisplayed -= OnHotelRoomsDisplayed;
        }

        protected override void Collect()
        {
            _labelField = Root.Q("HeaderField").Q("LabelField");
            _labelField.RegisterCallback<PointerUpEvent>(OnClicked_LabelField);

            _timeField = Root.Q("HeaderField").Q("TimeField");
            _timeIcon = _timeField.Q("Icon");
            _timeText = _timeField.Q("Text") as Label;

            var scrollView = Root.Q("ItemScroll") as ScrollView;
            Root.Remove(scrollView);

            _itemList = Root.Q("ItemList") as ListView;
            _itemList.Q("unity-content-container").SetFlexGrow(1);
            _itemList.Q<ScrollView>().verticalScrollerVisibility = ScrollerVisibility.Hidden;
            _itemList.fixedItemHeight = 700;
            _itemList.itemsSource = _rooms;
            _itemList.makeItem = () => new RoomSelectItemUI();
            _itemList.bindItem = (element, index) =>
            {
                var item = element as RoomSelectItemUI;
                item.OnBooked = OnRoomBooked;
                item.Apply(_rooms[index]);
            };
        }

        private void OnClicked_LabelField(PointerUpEvent evt)
        {
            Marker.OnViewPageSwitched?.Invoke(ViewType.InformationViewMainPage, true, false);
        }

        private void RebuildHistoryList()
        {
            _itemList.itemsSource = null;
            _itemList.itemsSource = _rooms;
            _itemList.Rebuild();
        }

        private void OnHotelRoomsDisplayed(UID hotelID, Room.StayType stayType, DateTime checkInTime, byte duration)
        {
            var unit = Main.Database.Hotels[_hotelID = hotelID];
            _rooms = unit.Rooms;
            _stayType = stayType;
            _checkInTime = checkInTime;
            _duration = duration;

            RebuildHistoryList();
        }

        private void OnRoomBooked(RoomUnit unit)
        {
            Marker.OnViewPageSwitched?.Invoke(ViewType.InformationViewPaymentPage, true, false);
            Marker.OnPaymentRequested?.Invoke(_hotelID, unit, _stayType, _checkInTime, _duration);
        }
    }
}