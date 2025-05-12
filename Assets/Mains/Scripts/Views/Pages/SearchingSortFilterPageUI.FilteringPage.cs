using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using static YNL.Checkotel.SearchingSortFilterPageUI;

namespace YNL.Checkotel
{
    public partial class SearchingSortFilterPageUI
    {
        public class FilteringPage : IInitializable, IResetable
        {
            public SortingSelectionType SortingType;
            public HotelFacility HotelFacility;

            private VisualElement _root;
            private VisualElement _filteringPage;
            private VisualElement _closeButton;
            private Button _resetButton;
            private VisualElement _applyButton;
            private PriceRangeField _priceRangeField;
            private VisualElement _reviewScoreField;
            private VisualElement _cleanlinessField;
            private VisualElement _hotelTypeField;
            private VisualElement _hotelFacilitiesList;

            public FilteringPage(VisualElement root)
            {
                _root = root;

                _filteringPage = _root.Q("FilteringPage");

                _closeButton = _filteringPage.Q("LabelField").Q("CloseButton");
                _closeButton.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);

                _resetButton = _filteringPage.Q("LabelField").Q("ResetButton") as Button;
                _resetButton.clicked += OnClicked_ResetButton;

                _applyButton = _filteringPage.Q("Toolbar").Q("ApplyButton");
                _applyButton.RegisterCallback<PointerDownEvent>(OnClicked_ApplyButton);
                _priceRangeField = new(_filteringPage);

                _reviewScoreField = _filteringPage.Q("FilterScroll").Q("ReviewScoreField").Q("SelectionField");
                _cleanlinessField = _filteringPage.Q("FilterScroll").Q("CleanlinessField").Q("SelectionField");
                _hotelTypeField = _filteringPage.Q("FilterScroll").Q("HotelTypeField").Q("SelectionField");

                _hotelFacilitiesList = _filteringPage.Q("FilterScroll").Q("HotelFacilitiesField").Q("SelectionList");

                Initialize();
                Reset();
            }
            ~FilteringPage()
            {
            }

            public void Initialize()
            {
                _reviewScoreField.Clear();
                _reviewScoreField.Add(new FilterPropertyButtonUI("≥ 4.5", FilterSelectionType.ReviewScore, FilterPropertyType.GE45));
                _reviewScoreField.Add(new FilterPropertyButtonUI("≥ 4.0", FilterSelectionType.ReviewScore, FilterPropertyType.GE40));
                _reviewScoreField.Add(new FilterPropertyButtonUI("≥ 3.5", FilterSelectionType.ReviewScore, FilterPropertyType.GE35));

                _cleanlinessField.Clear();
                _cleanlinessField.Add(new FilterPropertyButtonUI("5.0", FilterSelectionType.Cleanliness, FilterPropertyType.E50));
                _cleanlinessField.Add(new FilterPropertyButtonUI("≥ 4.9", FilterSelectionType.Cleanliness, FilterPropertyType.GE49));
                _cleanlinessField.Add(new FilterPropertyButtonUI("≥ 4.8", FilterSelectionType.Cleanliness, FilterPropertyType.GE48));

                _hotelTypeField.Clear();
                _hotelTypeField.Add(new FilterPropertyButtonUI("Flash sale", FilterSelectionType.HotelType, FilterPropertyType.FlashSale));
                _hotelTypeField.Add(new FilterPropertyButtonUI("Hot", FilterSelectionType.HotelType, FilterPropertyType.Hot));
                _hotelTypeField.Add(new FilterPropertyButtonUI("New", FilterSelectionType.HotelType, FilterPropertyType.New));
                _hotelTypeField.Add(new FilterPropertyButtonUI("Stamp", FilterSelectionType.HotelType, FilterPropertyType.Stamp));
                _hotelTypeField.Add(new FilterPropertyButtonUI("Discount", FilterSelectionType.HotelType, FilterPropertyType.Discount));
                _hotelTypeField.Add(new FilterPropertyButtonUI("Coupon", FilterSelectionType.HotelType, FilterPropertyType.Coupon));

                _hotelFacilitiesList.Clear();
                foreach (HotelFacility type in Enum.GetValues(typeof(HotelFacility)))
                {
                    _hotelFacilitiesList.Add(new FilteringSelectionItemUI(this, type));
                }
            }

            public void Reset()
            {
                (_reviewScoreField.Children().ToArray()[0] as FilterPropertyButtonUI).OnClicked__Button();
                (_cleanlinessField.Children().ToArray()[0] as FilterPropertyButtonUI).OnClicked__Button();
                (_hotelTypeField.Children().ToArray()[0] as FilterPropertyButtonUI).OnClicked__Button();
            }

            private void OnClicked_CloseButton(PointerDownEvent evt)
            {
            }

            private void OnClicked_ResetButton()
            {
            }

            private void OnClicked_ApplyButton(PointerDownEvent evt)
            {
            }
        }
    }
}