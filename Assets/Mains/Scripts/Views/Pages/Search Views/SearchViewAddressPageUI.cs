using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class SearchViewAddressPageUI : ViewPageUI
    {
        private VisualElement _background;
        private VisualElement _page;
        private VisualElement _closeButton;
        private TextField _addressInput;
        private VisualElement _nearMeButton;
        private ListView _historyList;

        private List<(string name, bool isBuilding)> _resultLocations = new();

        protected override void Collect()
        {
            _background = Root.Q("ScreenBackground");
            _background.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);
            _page = Root.Q("AddressPage");

            _closeButton = Root.Q("LabelField");
            _closeButton.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);

            _addressInput = Root.Q("SearchField").Q("SearchInput") as TextField;
            _addressInput.RegisterValueChangedCallback(OnValueChanged_AddressInput);
            _addressInput.RegisterCallback<FocusOutEvent>(OnFocusOut_AddressInput);

            _nearMeButton = Root.Q("NearMeButton");
            _nearMeButton.RegisterCallback<PointerDownEvent>(OnClicked_NearMeButton);

            var historyScroll = Root.Q("SearchingHistory").Q("HistoryScroll");
            historyScroll.SetDisplay(DisplayStyle.None);

            _historyList = Root.Q("SearchingHistory").Q("HistoryList") as ListView;
            _historyList.fixedItemHeight = 100;
            _historyList.itemsSource = _resultLocations;
            _historyList.makeItem = () => new SearchingHistoryItemUI();
            _historyList.bindItem = (element, index) =>
            {
                var item = element as SearchingHistoryItemUI;
                item.Apply(_resultLocations[index].name, _resultLocations[index].isBuilding);
                item.OnSelected = ApplyAddressResult;
            };
        }

        protected override void Initialize()
        {
            _resultLocations = Main.Runtime.SearchingAddressHistory.Select(i => (i, false)).ToList();
            RebuildHistoryList();
        }

        public override void OnPageOpened(bool isOpen)
        {
            if (isOpen)
            {
                _background.SetPickingMode(PickingMode.Position);
                _background.SetBackgroundColor(new Color(0.0865f, 0.0865f, 0.0865f, 0.725f));
                _page.SetTranslate(0, 0, true);
            }
            else
            {
                _background.SetBackgroundColor(Color.clear);
                _background.SetPickingMode(PickingMode.Ignore);
                _page.SetTranslate(0, 100, true);
            }
        }

        private void OnClicked_CloseButton(PointerDownEvent evt)
        {
            OnPageOpened(false);
        }

        private void OnFocusOut_AddressInput(FocusOutEvent evt)
        {

        }

        private void OnValueChanged_AddressInput(ChangeEvent<string> evt)
        {
            var value = _addressInput.value;

            if (value == string.Empty)
            {
                _resultLocations = Main.Runtime.SearchingAddressHistory.Select(i => (i, false)).ToList();
            }
            else
            {
                Func<string, bool> validLocations = i => Extension.Function.FuzzyContains(i, value);
                Func<KeyValuePair<UID, HotelUnit>, bool> validHotels = i => Extension.Function.FuzzyContains(i.Value.Description.Address, value);

                var locations = Main.Database.Locations.Where(validLocations).ToList();
                var hotels = Main.Database.Hotels.Where(validHotels).Select(p => p.Value.Description.Address).ToList();

                _resultLocations = locations.Select(i => (i, false)).ToList(); ;
                _resultLocations.AddRange(hotels.Select(i => (i, true)).ToList());
            }

            RebuildHistoryList();
        }

        private void OnSubmited_AddressInput()
        {
            Marker.OnAddressSearchSubmited?.Invoke(_addressInput.value);
        }

        private void OnClicked_NearMeButton(PointerDownEvent evt)
        {
            ApplyAddressResult("Ha Noi");
        }

        private void RebuildHistoryList()
        {
            _historyList.itemsSource = null;
            _historyList.itemsSource = _resultLocations;
            _historyList.Rebuild();
        }

        private void ApplyAddressResult(string result)
        {
            _addressInput.SetText(result);

            OnPageOpened(false);

            Marker.OnAddressSearchSubmited.Invoke(result);
        }
    }
}