using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.Extensions;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class SearchViewMainPageUI : ViewPageUI, ICollectible
    {
        [SerializeField] private SearchViewAddressPageUI _addressPageUI;
        [SerializeField] private SearchViewTimeRangePageUI _timeRangePageUI;

        private VisualElement _closeButton;
        private VisualElement _addressButton;
        private VisualElement _stayTypeField;
        private VisualElement _roomTypeField;
        private VisualElement _timeRangeButton;
        private Label _checkInTime;
        private Label _checkOutTime;
        private VisualElement _searchButton;

        protected override void VirtualAwake()
        {
            Marker.OnSystemStart += Collect;
        }

        private void OnDestroy()
        {
            Marker.OnSystemStart -= Collect;
        }

        public void Collect()
        {
            _closeButton = Root.Q("LabelField");
            _closeButton.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);

            _addressButton = Root.Q("SearchField");
            _addressButton.RegisterCallback<PointerDownEvent>(OnClicked_AddressButton);

            _stayTypeField = Root.Q("StayTypeField").Q("TimeField");
            _stayTypeField.Clear();
            _stayTypeField.Add(new SearchingSelectTypeUI("Hourly", "Hourly", "Hourly", true).SetStayType(SearchingStayType.Hourly));
            _stayTypeField.Add(new SearchingSelectTypeUI("Overnight", "Overnight (Filled)", "Overnight", false).SetStayType(SearchingStayType.Overnight));
            _stayTypeField.Add(new SearchingSelectTypeUI("Daily", "Daily", "Daily", false).SetStayType(SearchingStayType.Daily));

            _roomTypeField = Root.Q("RoomTypeField").Q("TypeField");
            _roomTypeField.Clear();
            _roomTypeField.Add(new SearchingSelectTypeUI("Bed", "Bed (Filled)", "Standard", true).SetRoomType(SearchingRoomType.Standard));
            _roomTypeField.Add(new SearchingSelectTypeUI("Family", "Family (Filled)", "Family", false).SetRoomType(SearchingRoomType.Family));
            _roomTypeField.Add(new SearchingSelectTypeUI("Business", "Business", "Business", false).SetRoomType(SearchingRoomType.Business));

            var timeRangeField = Root.Q("TimeRangeField");

            _timeRangeButton = timeRangeField.Q("TimeFIeld");
            _timeRangeButton.RegisterCallback<PointerDownEvent>(OnClicked_TimeRangeButton);

            _checkInTime = _timeRangeButton.Q("CheckInField").Q("Time") as Label;
            _checkInTime.RegisterCallback<PointerDownEvent>(OnClicked_CheckingButton);

            _checkOutTime = _timeRangeButton.Q("CheckOutField").Q("Time") as Label;
            _checkOutTime.RegisterCallback<PointerDownEvent>(OnClicked_CheckingButton);

            _searchButton = Root.Q("SearchButton");
            _searchButton.RegisterCallback<PointerDownEvent>(OnClicked_SearchButton);
        }

        private void OnClicked_CloseButton(PointerDownEvent evt)
        {
            Marker.OnViewPageSwitched?.Invoke(ViewType.MainView, ViewKey.MainViewHomePage, true);
        }

        private void OnClicked_AddressButton(PointerDownEvent evt)
        {
            _addressPageUI.Root.SetTranslate(0, 0, true);
        }

        private void OnClicked_TimeRangeButton(PointerDownEvent evt)
        {
            _timeRangePageUI.Root.SetTranslate(0, 0, true);
        }

        private void OnClicked_CheckingButton(PointerDownEvent evt)
        {
        }

        private void OnClicked_SearchButton(PointerDownEvent evt)
        {
            Marker.OnViewPageSwitched?.Invoke(ViewType.SearchView, ViewKey.SearchViewResultPage, true);
        }
    }
}