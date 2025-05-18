using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public enum PreviewListFilterType : byte
    {
        NewHotels, MostPopular, HighRated, LuxuryStays, FamilyFriendlyHotels, ExceptionalChoices
    }

    public class HotelPreviewListUI : VisualElement
    {
        private const string _rootClass = "hotel-preview-list";
        private const string _labelFieldClass = _rootClass + "__label-field";
        private const string _labelClass = _rootClass + "__label";
        private const string _seeMoreButtonClass = _rootClass + "__see-more-button";
        private const string _previewListClass = _rootClass + "__preview-list";

        private VisualElement _labelField;
        private Label _label;
        private Label _seeMoreButton;
        private ScrollView _previewList;

        public HotelPreviewListUI(PreviewListFilterType type, bool isMini = false)
        {
            this.AddStyle(Main.Resources.Styles["StyleVariableUI"]);
            this.AddStyle(Main.Resources.Styles["HotelPreviewListUI"]);
            this.AddClass(_rootClass);

            _labelField = new VisualElement().AddClass(_labelFieldClass);
            this.AddElements(_labelField);

            _label = new Label(Extension.Value.ToSentenceCase(type)).AddClass(_labelClass);
            _labelField.AddElements(_label);

            _seeMoreButton = new Label("See more").AddClass(_seeMoreButtonClass);
            _labelField.AddElements(_seeMoreButton);

            _previewList = new ScrollView().AddClass(_previewListClass);
            _previewList.mode = ScrollViewMode.Horizontal;
            _previewList.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
            _previewList.verticalScrollerVisibility = ScrollerVisibility.Hidden;
            this.AddElements(_previewList);

            Initialize(isMini);
        }

        private void Initialize(bool isMini)
        {
            for (int i = 0; i < 10; i++)
            {
                _previewList.AddElements(new HotelPreviewItemUI(100000001, isMini));
            }
        }

        public HotelPreviewListUI SetAsLastItem()
        {
            this.SetMarginBottom(275);

            return this;
        }
    }
}