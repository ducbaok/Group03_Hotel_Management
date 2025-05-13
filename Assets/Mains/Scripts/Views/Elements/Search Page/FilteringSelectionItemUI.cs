using System;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    using FilteringPage = SearchingSortFilterPageUI.FilteringPage;

    public class FilteringSelectionItemUI : VisualElement
    {
        public static Action<HotelFacility> OnSelected { get; set; }

        private const string _rootClass = "toggle-property-item";
        private const string _labelClass = _rootClass + "__label";
        private const string _toggleClass = _rootClass + "__toggle";
        private const string _selected = "selected";

        private Label _label;
        private VisualElement _toggle;

        private bool _isSelected = false;
        private FilteringPage _filteringPage;
        private HotelFacility _hotelFacility;

        public FilteringSelectionItemUI(FilteringPage filteringPage, HotelFacility type)
        {
            OnSelected += UpdateUI;

            _filteringPage = filteringPage;
            _hotelFacility = type;

            this.AddStyle(Main.Resources.Styles["StyleVariableUI"]);
            this.AddStyle(Main.Resources.Styles["TogglePropertyItemUI"]);
            this.AddClass(_rootClass);
            this.RegisterCallback<PointerDownEvent>(OnClicked__Toggle);

            _label = new(Extension.Value.ToSentenceCase(type));
            _label.AddClass(_labelClass);
            this.AddElements(_label);

            _toggle = new();
            _toggle.AddClass(_toggleClass);
            this.AddElements(_toggle);
        }
        ~FilteringSelectionItemUI()
        {
            OnSelected -= UpdateUI;
        }

        public void SetAsLastItem()
        {
            this.style.borderBottomWidth = 0;
        }

        public void OnClicked__Toggle(PointerDownEvent evt = null)
        {
            _isSelected = !_isSelected;
            _toggle.EnableClass(_isSelected, _selected);

            if (_hotelFacility == HotelFacility.None && _isSelected)
            {
                _filteringPage.HotelFacility = _hotelFacility;
            }
            else if (_isSelected)
            {
                _filteringPage.HotelFacility |= _hotelFacility;
                _filteringPage.HotelFacility &= ~HotelFacility.None;
            }
            else
            {
                _filteringPage.HotelFacility &= ~_hotelFacility;    
                if (_filteringPage.HotelFacility == 0)
                {
                    _filteringPage.HotelFacility = HotelFacility.None;
                }
            }

            OnSelected?.Invoke(_hotelFacility);
        }

        private void UpdateUI(HotelFacility hotelFacility)
        {
            if (!(_isSelected && (_hotelFacility == HotelFacility.None) != (hotelFacility == HotelFacility.None))) return;

            _isSelected = false;
            _toggle.EnableClass(_isSelected, _selected);
        }
    }
}