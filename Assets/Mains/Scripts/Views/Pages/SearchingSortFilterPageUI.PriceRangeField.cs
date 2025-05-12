using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using YNL.Utilities.Extensions;

namespace YNL.Checkotel
{
    public partial class SearchingSortFilterPageUI
    {
        public class PriceRangeField : IResetable
        {
            public static (int Min, int Max) PriceRange = (5, 1000);

            private Label _minLabel;
            private Label _maxLabel;
            private MinMaxSlider _slider;

            public PriceRangeField(VisualElement filteringPage)
            {
                var priceRangeView = filteringPage.Q("FilterScroll").Q("PriceRangeView");

                _minLabel = priceRangeView.Q("PriceField").Q("MinPriceBox").Q("Text") as Label;

                _maxLabel = priceRangeView.Q("PriceField").Q("MaxPriceBox").Q("Text") as Label;

                _slider = priceRangeView.Q("PriceSlider") as MinMaxSlider;
                _slider.RegisterValueChangedCallback(OnValueChanged_Slider);

                Reset();
            }

            public void Reset()
            {
                _slider.value = new(0, 1);
            }

            private void OnValueChanged_Slider(ChangeEvent<Vector2> evt)
            {
                (float min, float max) ratio = (evt.newValue.x, evt.newValue.y);

                int minPrice = Mathf.RoundToInt(ratio.min.Remap(new(0, 1), new(PriceRange.Min, PriceRange.Max)));
                int maxPrice = Mathf.RoundToInt(ratio.max.Remap(new(0, 1), new(PriceRange.Min, PriceRange.Max)));

                _minLabel.text = $"<b>{minPrice.ToString("N0")}$</b>";
                _maxLabel.text = maxPrice == PriceRange.Max ? $"<size=100>∞</size>" : $"<b>{maxPrice.ToString("N0")}$</b>";
            }
        }
    }
}