using UnityEngine.UIElements;
using YNL.Utilities.Addons;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public partial class InformationViewMainPage
    {
        public enum PriceFieldType : byte
        {
            HourlyTime, OvernightTime, DailyTime
        }

        public class PriceField : ICollectible
        {
            private VisualElement _root;

            private VisualElement _timeField;
            private VisualElement _timeIcon;
            private Label _timeText;
            private Label _originalPrice;
            private Label _lastPrice;
            private Button _chooseButton;

            public PriceField(VisualElement root)
            {
                _root = root;

                Collect();
            }

            public void Collect()
            {
                var bottomBar = _root.Q("BottomBar");

                _timeField = bottomBar.Q("TimeField");

                _timeIcon = _timeField.Q("Icon");

                _timeText = _timeField.Q("Text") as Label;

                var priceArea = bottomBar.Q("PriceArea");

                _originalPrice = priceArea.Q("PriceField").Q("OriginalPrice") as Label;

                _lastPrice = priceArea.Q("PriceField").Q("LastPrice") as Label;

                _chooseButton = _lastPrice.Q("ChooseButton") as Button;
            }
        }

        public class NameView
        {
            private VisualElement _badgeField;
            private Label _nameText;
            private Label _addressText;

            public NameView(VisualElement contentContainer)
            {
                _badgeField = contentContainer.Q("BadgeField");
                _nameText = contentContainer.Q("NameText") as Label;
                _addressText = contentContainer.Q("AddressText") as Label;
            }

            public void Apply(string name, string address)
            {
                _nameText.SetText(name);
                _addressText.SetText(address);
            }
        }

        public class ReviewView
        {
            private Label _ratingText;
            private Label _reviewAmount;
            private Label _seeMoreButton;
            private ScrollView _reviewScroll;
            private Label _emptyLabel;

            public ReviewView(VisualElement contentContainer)
            {
                var reviewView = contentContainer.Q("ReviewView");
                var scoreField = reviewView.Q("ScoreField");

                _ratingText = scoreField.Q("Rating") as Label;
                _reviewAmount = scoreField.Q("Amount") as Label;
                _seeMoreButton = scoreField.Q("SeeMore") as Label;
                _seeMoreButton.RegisterCallback<PointerDownEvent>(OnClicked_SeeMoreButton);

                _reviewScroll = reviewView.Q("ReviewScroll") as ScrollView;

                _emptyLabel = reviewView.Q("EmptyLabel") as Label;
            }

            public void Apply(UID id)
            {
                var review = Main.Database.Hotels[id].Review;

                _ratingText.SetText(review.AverageRating.ToString("0.0"));
                _reviewAmount.SetText(review.FeebackAmount.ToString());

                bool emptyFeedback = review.FeebackAmount == 0;

                _emptyLabel.SetDisplay(emptyFeedback ? DisplayStyle.Flex : DisplayStyle.None);
                _reviewScroll.SetDisplay(emptyFeedback ? DisplayStyle.None : DisplayStyle.Flex);

                if (!emptyFeedback)
                {
                    _reviewScroll.Clear();
                    for (byte i = 0; i < review.Feedbacks.Count; i++)
                    {
                        if (i >= 5) break;

                    }
                }
            }

            private void OnClicked_SeeMoreButton(PointerDownEvent evt)
            {
                Marker.OnViewPageSwitched?.Invoke(ViewType.InformationViewReviewPage, true, false);
            }
        }

        public class FacilityView
        {
            public class FacilityItem : VisualElement
            {
                private VisualElement _icon;
                private Label _text;

                public FacilityItem(VisualElement item)
                {
                    _icon = item.Q("Icon");
                    _text = item.Q("Text") as Label;
                }

                public void Apply(HotelFacility facility)
                {
                    var field = facility.GetHotelFacilitiesField();

                    _icon.SetBackgroundImage(field.Icon);
                    _text.SetText(field.Name);
                }
            }

            private Label _seeMoreButton;
            private VisualElement _facilityField;
            private FacilityItem[] _facilityItems = new FacilityItem[4];

            public FacilityView(VisualElement contentContainer)
            {
                var facilityView = contentContainer.Q("FacilityView");

                _seeMoreButton = facilityView.Q("LabelField").Q("SeeMore") as Label;
                _facilityField = facilityView.Q("FacilityField");

                for (byte i = 0; i < _facilityItems.Length; i++)
                {
                    _facilityItems[i] = new(_facilityField.Q($"FacilityItem{i}"));
                }
            }

            public void Apply(HotelUnit unit)
            {
                var facilities = unit.GetHotelFacilities();

                for (byte i = 0; i < _facilityItems.Length; i++)
                {
                    if (i < facilities.Count)
                    {
                        _facilityItems[i].SetDisplay(DisplayStyle.Flex);
                        _facilityItems[i].Apply(facilities[i]);
                    }
                    else
                    {
                        _facilityItems[i].SetDisplay(DisplayStyle.None);
                    }
                }
            }
        }

        public class DescriptionField
        {
            private Label _descriptionText;

            private bool _isExpanded = false;

            public DescriptionField(VisualElement contentContainer)
            {
                _descriptionText = contentContainer.Q("DescriptionField").Q("DescriptionText") as Label;
                _descriptionText.RegisterCallback<PointerDownEvent>(OnClicked_DescriptionText);
            }

            public void Apply(string description)
            {
                _descriptionText.SetText(description);
            }

            private void OnClicked_DescriptionText(PointerDownEvent evt)
            {
                _isExpanded = !_isExpanded;

                if (_isExpanded)
                {
                    _descriptionText.SetMaxHeight(StyleKeyword.Auto);
                }
                else
                {
                    _descriptionText.SetMaxHeight(175);
                }
            }
        }

        public class TimeField
        {

        }
    }
}