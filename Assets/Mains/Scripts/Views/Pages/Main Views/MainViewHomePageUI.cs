using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public class MainViewHomePageUI : ViewPageUI
    {
        private VisualElement _searchField;
        private VisualElement _notificationButton;
        private ScrollView _avalableFilterScroll;
        private ScrollView _typeFilterScroll;
        private ScrollView _pageScroll;

        protected override void Collect()
        {
            _searchField = Root.Q("TopBar").Q("SearchField");
            _searchField.RegisterCallback<PointerDownEvent>(OnClicked__SearchField);

            _notificationButton = Root.Q("TopBar").Q("NotificationButton");
            _notificationButton.RegisterCallback<PointerDownEvent>(OnClicked__NotificationButton);

            _pageScroll = Root.Q("ScrollView") as ScrollView;

            _avalableFilterScroll = _pageScroll.Q("BackgroundContainer").Q("AvalableScroll") as ScrollView;

            _typeFilterScroll = _pageScroll.Q("BackgroundContainer").Q("FilterScroll") as ScrollView;
        }

        protected override void Initialize()
        {
            _avalableFilterScroll.Clear();
            _avalableFilterScroll.Add(new SuggestFilterButtonUI(true, Main.Resources.Icons["Star"], "Romantic", SuggestFilterType.Romantic));
            _avalableFilterScroll.Add(new SuggestFilterButtonUI(true, Main.Resources.Icons["Star"], "With Pool", SuggestFilterType.WithPool));
            _avalableFilterScroll.Add(new SuggestFilterButtonUI(true, Main.Resources.Icons["Star"], "Homestay", SuggestFilterType.Homestay));

            _typeFilterScroll.Clear();
            _typeFilterScroll.Add(new SuggestFilterButtonUI(false, Main.Resources.Icons["Star"], "<b>Near You</b>\n<size=30>One step to the sky</size>", SuggestFilterType.NearYou));
            _typeFilterScroll.Add(new SuggestFilterButtonUI(false, Main.Resources.Icons["Star"], "<b>Hourly</b>\n<size=30>Enjoy every seconds</size>", SuggestFilterType.Hourly));
            _typeFilterScroll.Add(new SuggestFilterButtonUI(false, Main.Resources.Icons["Star"], "<b>Overnight</b>\n<size=30>Comfort like home</size>", SuggestFilterType.Overnight));
            _typeFilterScroll.Add(new SuggestFilterButtonUI(false, Main.Resources.Icons["Star"], "<b>Daily</b>\n<size=30>Every day is joyful</size>", SuggestFilterType.Daily));

            _pageScroll.Add(new HotelPreviewListUI(PreviewListFilterType.MostPopular));
            _pageScroll.Add(new HotelPreviewListUI(PreviewListFilterType.NewHotels, true).SetAsLastItem());
        }

        private void OnClicked__SearchField(PointerDownEvent evt)
        {
            Marker.OnViewPageSwitched?.Invoke(ViewType.SearchView, ViewKey.SearchViewMainPage, true);
        }

        private void OnClicked__NotificationButton(PointerDownEvent evt)
        {
            Marker.OnNotificationViewOpened?.Invoke();
        }
    }
}