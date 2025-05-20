using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public partial class InformationViewMainPage : ViewPageUI
    {
        private VisualElement _backButton;
        private VisualElement _favoriteButton;
        private VisualElement _shareButton;
        private PriceField _priceField;
        private VisualElement _imageView;
        private NameView _nameView;
        private ReviewView _reviewView;
        private FacilityView _facilityView;
        private DescriptionField _descriptionField;
        private VisualElement _timeField;
        private VisualElement _hotelPolicy;
        private VisualElement _cancellationPolicy;

        private UID _hotelID;

        protected override void VirtualAwake()
        {
            Marker.OnHotelInformationDisplayed += OnHotelInformationDisplayed;
        }

        private void OnDestroy()
        {
            Marker.OnHotelInformationDisplayed -= OnHotelInformationDisplayed;
        }

        protected override void Collect()
        { 
            _backButton = Root.Q("TopBar").Q("BackButton");
            _backButton.RegisterCallback<PointerDownEvent>(OnClicked_BackButton);

            _favoriteButton = Root.Q("TopBar").Q("FavoriteButton");
            _favoriteButton.RegisterCallback<PointerDownEvent>(OnClicked_FavoriteButton);

            _shareButton = Root.Q("TopBar").Q("ShareButton");

            _priceField = new(Root);

            var contentContainer = Root.Q("ContentScroll").Q("unity-content-container");

            _imageView = contentContainer.Q("ImageView");

            _nameView = new(contentContainer.Q("NameView"));

            _reviewView = new(contentContainer);

            _facilityView = new(contentContainer);

            _descriptionField = new(contentContainer);

            _timeField = contentContainer.Q("TimeField");

            _hotelPolicy = contentContainer.Q("HotelPolicy");

            _cancellationPolicy = contentContainer.Q("CancellationPolicy");
        }

        private void OnClicked_BackButton(PointerDownEvent evt)
        {

        }

        private void OnClicked_FavoriteButton(PointerDownEvent evt)
        {

        }

        private void OnHotelInformationDisplayed(UID id)
        {
            _hotelID = id;

            var unit = Main.Database.Hotels[id];

            _nameView.Apply(unit.Description.Name, unit.Description.Address);
            _reviewView.Apply(id);
            _facilityView.Apply(unit);
            _descriptionField.Apply(unit.Description.Description);
        }
    }
}