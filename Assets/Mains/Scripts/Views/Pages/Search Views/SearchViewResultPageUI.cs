using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.Addons;
using YNL.Utilities.Extensions;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    using static YNL.Checkotel.Room;
    using HotelList = SerializableDictionary<UID, HotelUnit>;

    public class SearchViewResultPageUI : ViewPageUI
    {
        private static HotelList _hotels => Main.Database.Hotels;

        [SerializeField] private SearchViewSortPageUI _sortPage;
        [SerializeField] private SearchViewFilterPageUI _filterPage;

        private VisualElement _searchBar;
        private Label _searchText;
        private VisualElement _resultScroll;
        private ListView _resultList;
        private VisualElement _sortButton;
        private VisualElement _filterButton;
        private Label _emptyResultLabel;

        private List<UID> _resultIDs = new();

        protected override void VirtualAwake()
        {
            Marker.OnSearchingResultRequested += OnSearchingResultRequested;
        }

        private void OnDestroy()
        {
            Marker.OnSearchingResultRequested -= OnSearchingResultRequested;
        }

        protected override void Collect()
        {
            var resultPage = Root.Q("SearchingResultPage");

            _searchBar = resultPage.Q("SearchBar");
            _searchBar.RegisterCallback<PointerUpEvent>(OnClicked_SearchBar);

            _searchText = _searchBar.Q("SearchField").Q("SearchText") as Label;

            _resultScroll = Root.Q("ResultScroll");
            _resultScroll.SetDisplay(DisplayStyle.None);

            _resultList = new ListView().SetFlexGrow(1).SetMarginTop(50);
            _resultList.Q("unity-content-container").SetFlexGrow(1);
            _resultList.Q<ScrollView>().verticalScrollerVisibility = ScrollerVisibility.Hidden;
            _resultList.fixedItemHeight = 700;
            _resultList.itemsSource = _resultIDs;
            _resultList.makeItem = () => new SearchingResultItemUI();
            _resultList.bindItem = (element, index) =>
            {
                var item = element as SearchingResultItemUI;
                item.Apply(_resultIDs[index], Main.Runtime.StayType);
            };
            resultPage.Add(_resultList);

            _sortButton = resultPage.Q("ResultField").Q("SortingButton");
            _sortButton.RegisterCallback<PointerUpEvent>(OnClicked_SortButton);

            _filterButton = resultPage.Q("ResultField").Q("FilteringButton");
            _filterButton.RegisterCallback<PointerUpEvent>(OnClicked_FilterButton);

            _emptyResultLabel = resultPage.Q("EmptyResultLabel") as Label;
        }

        protected override void Refresh()
        {
            RebuildHistoryList();
        }

        private void OnClicked_SearchBar(PointerUpEvent evt)
        {
            Marker.OnViewPageSwitched?.Invoke(ViewType.SearchViewMainPage, true, false);
        }

        private void OnClicked_SortButton(PointerUpEvent evt)
        {
            _sortPage.OnPageOpened(true, true);
        }

        private void OnClicked_FilterButton(PointerUpEvent evt)
        {
            _filterPage.OnPageOpened(true, true);
        }

        private void RebuildHistoryList()
        {
            _resultList.itemsSource = null;
            _resultList.itemsSource = _resultIDs;
            _resultList.Rebuild();
        }

        private void OnSearchingResultRequested(string address, Room.RoomType roomType)
        {
            var timeRangeText = Main.Runtime.CheckInTime.GetCheckingTimeText(Main.Runtime.Duration);
            var addressText = string.IsNullOrEmpty(address) ? "Anywhere" : address;
            _searchText.SetText($"<b>{addressText}</b>\r\n<size=35>{Main.Runtime.StayType} • {timeRangeText.In} - {timeRangeText.Out}</size>");

            _resultIDs.Clear();

            foreach (var pair in _hotels)
            {
                var unit = pair.Value;
                var matchingArray = new bool[]
                {
                    unit.Description.Address.FuzzyContains(address) || unit.Description.Name.FuzzyContains(address),
                    unit.Rooms.Any(i => i.Description.Restriction.StayType == Main.Runtime.StayType),
                    unit.Rooms.Any(i => i.Description.Restriction.RoomType == roomType),
                    pair.Key.IsValidTimeRange(Main.Runtime.StayType, Main.Runtime.CheckInTime, Main.Runtime.Duration)
                };  

                if (matchingArray.All(i => i == true))
                {
                    _resultIDs.Add(pair.Key);
                }
            }

            bool emptyResult = _resultIDs.IsEmpty();

            _emptyResultLabel.SetDisplay(emptyResult ? DisplayStyle.Flex : DisplayStyle.None);
            _resultList.SetDisplay(emptyResult ? DisplayStyle.None : DisplayStyle.Flex);

            if (!emptyResult) RebuildHistoryList();
        }
    }
}