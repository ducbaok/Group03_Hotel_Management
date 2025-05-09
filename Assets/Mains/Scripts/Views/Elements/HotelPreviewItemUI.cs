using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public partial class HotelPreviewItemUI : VisualElement
    {
        private const string _rootClass = "hotel-preview-item";
        private const string _imageClass = _rootClass + "__image";
        private const string _infoClass = _rootClass + "__info";
        private const string _infoNameClass = _rootClass + "__info-name";
        private const string _infoLocationClass = _rootClass + "__info-location";
        private const string _infoLocationIconClass = _rootClass + "__info-location-icon";
        private const string _infoLocationTextClass = _rootClass + "__info-location-text";
        private const string _infoPriceClass = _rootClass + "__info-price";
        private const string _infoPriceTextClass = _rootClass + "__info-price-text";
        private const string _infoRatingClass = _rootClass + "__info-rating";
        private const string _infoRatingIconClass = _rootClass + "__info-rating-icon";
        private const string _infoRatingTextClass = _rootClass + "__info-rating-text";
        private const string _infoDiscountClass = _rootClass + "__info-discount";
        private const string _infoDiscountFrameClass = _rootClass + "__info-discount-frame";
        private const string _infoDiscountAmountClass = _rootClass + "__info-discount-amount";
        private const string _infoDiscountTextClass = _rootClass + "__info-discount-text";

        private VisualElement _previewImage;
        private VisualElement _previewInfo;
        private Label _nameLabel;
        private VisualElement _locationField;
        private VisualElement _locationIcon;
        private Label _locationText;
        private VisualElement _priceField;
        private Label _priceText;
        private VisualElement _ratingField;
        private VisualElement _ratingIcon;
        private Label _ratingText;
        private VisualElement _discountField;
        private VisualElement _discountFrame;
        private Label _discountAmount;
        private Label _discountText;

        public HotelPreviewItemUI()
        {
            Initialize();
            AddValue();
        }

        private void Initialize()
        {
            this.styleSheets.Add(Main.Resources.Styles["StyleVariableUI"]);
            this.styleSheets.Add(Main.Resources.Styles["HotelPreviewItemUI"]);
            this.AddToClassList(_rootClass);

            _previewImage = new VisualElement();
            _previewImage.AddToClassList(_imageClass);

            _previewInfo = new VisualElement();
            _previewInfo.AddToClassList(_infoClass);

            _nameLabel = new Label();
            _nameLabel.AddToClassList(_infoNameClass);

            _locationField = new VisualElement();
            _locationField.AddToClassList(_infoLocationClass);

            _locationIcon = new VisualElement();
            _locationIcon.AddToClassList(_infoLocationIconClass);

            _locationText = new Label();
            _locationText.AddToClassList(_infoLocationTextClass);

            _priceField = new VisualElement();
            _priceField.AddToClassList(_infoPriceClass);

            _priceText = new Label();
            _priceText.AddToClassList(_infoPriceTextClass);

            _ratingField = new VisualElement();
            _ratingField.AddToClassList(_infoRatingClass);

            _ratingIcon = new VisualElement();
            _ratingIcon.AddToClassList(_infoRatingIconClass);

            _ratingText = new Label();
            _ratingText.AddToClassList(_infoRatingTextClass);

            _discountField = new VisualElement();
            _discountField.AddToClassList(_infoDiscountClass);

            _discountFrame = new VisualElement();
            _discountFrame.AddToClassList(_infoDiscountFrameClass);

            _discountAmount = new Label();
            _discountAmount.AddToClassList(_infoDiscountAmountClass);

            _discountText = new Label();
            _discountText.AddToClassList(_infoDiscountTextClass);

            this.Add(_previewImage);
            this.Add(_previewInfo);

            _previewInfo.Add(_nameLabel);
            _previewInfo.Add(_locationField);
            _previewInfo.Add(_priceField);
            _previewInfo.Add(_discountField);

            _locationField.Add(_locationIcon);
            _locationField.Add(_locationText);

            _priceField.Add(_priceText);
            _priceField.Add(_ratingField);

            _ratingField.Add(_ratingIcon);
            _ratingField.Add(_ratingText);

            _discountField.Add(_discountFrame);
            _discountFrame.Add(_discountAmount);
            _discountField.Add(_discountText);
        }

        public void AddValue()
        {
            int discountPercentage = Random.Range(0, 50);

            _previewImage.style.backgroundImage = Resources.Load<Texture2D>("Images/Starting Background 1");

            _nameLabel.text = "Grand Azure Hotel";
            _locationText.text = "Paris, France";
            var price = Random.Range(80, 1000);
            _priceText.text = $"<b><color=#FED1A7>${price * (1 - (discountPercentage / 100f))}</color></b> <s>${price}</s> <b><size=30>/ 2 days</size></b>";
            _ratingText.text = Random.Range(3.0f, 5.0f).ToString("F1");

            if (discountPercentage > 0)
            {
                _discountAmount.text = $"-{discountPercentage}%";
                _discountText.text = "OFF";
                _discountField.style.display = DisplayStyle.Flex;
            }
            else
            {
                _discountField.style.display = DisplayStyle.None;
            }
        }
    }
}