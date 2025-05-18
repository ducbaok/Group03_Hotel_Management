using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.Extensions;
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
        private ScrollView _historyScroll;

        protected override void Collect()
        {
            _background = Root.Q("ScreenBackground");
            _background.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);
            _page = Root.Q("AddressPage");

            _closeButton = Root.Q("LabelField");
            _closeButton.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);

            _addressInput = Root.Q("SearchField").Q("SearchInput") as TextField;
            _addressInput.RegisterValueChangedCallback(OnValueChanged_AddressInput);

            _nearMeButton = Root.Q("NearMeButton");
            _nearMeButton.RegisterCallback<PointerDownEvent>(OnClicked_NearMeButton);

            _historyScroll = Root.Q("SearchingHistory").Q("HistoryScroll") as ScrollView;
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

        private void OnValueChanged_AddressInput(ChangeEvent<string> evt)
        {

        }

        private void OnClicked_NearMeButton(PointerDownEvent evt)
        {
        }
    }
}