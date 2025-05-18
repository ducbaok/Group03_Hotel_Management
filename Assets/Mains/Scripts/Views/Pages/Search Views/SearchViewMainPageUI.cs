using System;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class SearchViewMainPageUI : ViewPageUI
    {
        [SerializeField] private SearchViewAddressPageUI _addressPageUI;
        [SerializeField] private SearchViewTimeRangePageUI _timeRangePageUI;

        private VisualElement _closeButton;
        private VisualElement _addressButton;
        private Label _addressText;
        private VisualElement _stayTypeField;
        private VisualElement _roomTypeField;
        private VisualElement _timeRangeButton;
        private Label _checkInTime;
        private Label _checkOutTime;
        private VisualElement _searchButton;

        private string _addressValue;
        private (SearchingStayType stayType, SearchingRoomType roomType) _searchingType;
        private (DateTime checkInTime, byte duration) _timeRange;

        protected override void VirtualAwake()
        {
            Marker.OnAddressSearchSubmited += OnAddressSearchSubmited;
            Marker.OnTimeRangeSubmited += OnTimeRangeSubmited;
        }

        private void OnDestroy()
        {
            Marker.OnAddressSearchSubmited -= OnAddressSearchSubmited;
            Marker.OnTimeRangeSubmited -= OnTimeRangeSubmited;
        }

        protected override void Collect()
        {
            _closeButton = Root.Q("LabelField");
            _closeButton.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);

            _addressButton = Root.Q("SearchField");
            _addressButton.RegisterCallback<PointerDownEvent>(OnClicked_AddressButton);

            _addressText = _addressButton.Q("Label") as Label;

            _stayTypeField = Root.Q("StayTypeField").Q("TimeField");

            _roomTypeField = Root.Q("RoomTypeField").Q("TypeField");

            var timeRangeField = Root.Q("TimeRangeField");

            _timeRangeButton = timeRangeField.Q("TimeField");
            _timeRangeButton.RegisterCallback<PointerDownEvent>(OnClicked_TimeRangeButton);

            _checkInTime = _timeRangeButton.Q("CheckInField").Q("Time") as Label;
            _checkInTime.RegisterCallback<PointerDownEvent>(OnClicked_CheckingButton);

            _checkOutTime = _timeRangeButton.Q("CheckOutField").Q("Time") as Label;
            _checkOutTime.RegisterCallback<PointerDownEvent>(OnClicked_CheckingButton);

            _searchButton = Root.Q("SearchButton");
            _searchButton.RegisterCallback<PointerDownEvent>(OnClicked_SearchButton);
        }

        protected override void Initialize()
        {
            _stayTypeField.Clear();
            _stayTypeField.Add(new SearchingSelectTypeUI("Hourly", OnSearchingTypeSelected, true).SetStayType(SearchingStayType.Hourly));
            _stayTypeField.Add(new SearchingSelectTypeUI("Overnight", OnSearchingTypeSelected, false).SetStayType(SearchingStayType.Overnight));
            _stayTypeField.Add(new SearchingSelectTypeUI("Daily", OnSearchingTypeSelected, false).SetStayType(SearchingStayType.Daily));

            _roomTypeField.Clear();
            _roomTypeField.Add(new SearchingSelectTypeUI("Standard", OnSearchingTypeSelected, true).SetRoomType(SearchingRoomType.Standard));
            _roomTypeField.Add(new SearchingSelectTypeUI("Family", OnSearchingTypeSelected, false).SetRoomType(SearchingRoomType.Family));
            _roomTypeField.Add(new SearchingSelectTypeUI("Business", OnSearchingTypeSelected, false).SetRoomType(SearchingRoomType.Business));
        }

        private void OnClicked_CloseButton(PointerDownEvent evt)
        {
            Marker.OnViewPageSwitched?.Invoke(ViewType.MainView, ViewKey.MainViewHomePage, true);
        }

        private void OnClicked_AddressButton(PointerDownEvent evt)
        {
            _addressPageUI.OnPageOpened(true);
        }

        private void OnClicked_TimeRangeButton(PointerDownEvent evt)
        {
            _timeRangePageUI.OnPageOpened(true);
        }

        private void OnClicked_CheckingButton(PointerDownEvent evt)
        {
        }

        private void OnClicked_SearchButton(PointerDownEvent evt)
        {
            Marker.OnViewPageSwitched?.Invoke(ViewType.SearchView, ViewKey.SearchViewResultPage, true);
        }

        private void OnSearchingTypeSelected(bool isStayType, SearchingStayType stayType, SearchingRoomType roomType)
        {
            if (isStayType)
            {
                _searchingType.stayType = stayType;
            }
            else
            {
                _searchingType.roomType = roomType;
            }
        }

        private void OnAddressSearchSubmited(string value)
        {
            _addressValue = value;
            _addressText.SetText(value);
        }

        private void OnTimeRangeSubmited(DateTime checkInTime, byte duration)
        {
            _checkInTime.text = checkInTime.ToString("dd/MM, HH:mm");
            _checkOutTime.text = checkInTime.AddHours(duration).ToString("dd/MM, HH:mm");
        }
    }
}