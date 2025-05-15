using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.Extensions;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class SearchViewAddressPageUI : ViewPageUI, ICollectible
    {
        private VisualElement _closeButton;
        private TextField _addressInput;
        private VisualElement _nearMeButton;
        private ScrollView _historyScroll;

        protected override void VirtualAwake()
        {
            Marker.OnSystemStart += Collect;
        }

        private void OnDestroy()
        {
            Marker.OnSystemStart -= Collect;
        }

        public void Collect()
        {
            _closeButton = Root.Q("LabelField");
            _closeButton.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);

            _addressInput = Root.Q("SearchField").Q("SearchInput") as TextField;
            _addressInput.RegisterValueChangedCallback(OnValueChanged_AddressInput);

            _nearMeButton = Root.Q("NearMeButton");
            _nearMeButton.RegisterCallback<PointerDownEvent>(OnClicked_NearMeButton);

            _historyScroll = Root.Q("SearchingHistory").Q("HistoryScroll") as ScrollView;
        }

        private void OnClicked_CloseButton(PointerDownEvent evt)
        {
            Root.SetDisplay(DisplayStyle.None);
        }

        private void OnValueChanged_AddressInput(ChangeEvent<string> evt)
        {

        }

        private void OnClicked_NearMeButton(PointerDownEvent evt)
        {
        }
    }
}