using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public enum SuggestFilterType : byte
    {
        Romantic, WithPool, Homestay,
        NearYou, Hourly, Overnight, Daily,
    }

    public class SuggestFilterButtonUI : VisualElement
    {
        private const string _rootClass = "suggest-filter-button";
        private const string _iconClass = _rootClass + "__icon";
        private const string _labelClass = _rootClass + "__label";
        private const string _typeFilterClass = "type-filter";

        private VisualElement _icon;
        private Label _label;

        private SuggestFilterType _type;

        public SuggestFilterButtonUI(bool isAvailableFilter, Texture2D icon, string label, SuggestFilterType type)
        {
            _type = type;

            this.styleSheets.Add(Main.Resources.Styles["StyleVariableUI"]);
            this.styleSheets.Add(Main.Resources.Styles["SuggestFilterButtonUI"]);
            this.AddToClassList(_rootClass);
            this.EnableInClassList(_typeFilterClass, !isAvailableFilter);

            _icon = new();
            _icon.AddToClassList(_iconClass);
            _icon.EnableInClassList(_typeFilterClass, !isAvailableFilter);
            _icon.style.backgroundImage = icon;
            this.Add(_icon);

            _label = new();
            _label.AddToClassList(_labelClass);
            _label.text = label;
            this.Add(_label);


            this.RegisterCallback<PointerDownEvent>(OnClicked_Button);
        }

        private void OnClicked_Button(PointerDownEvent evt)
        {
            Marker.OnSuggestFilterSelected?.Invoke(_type);
        }
    }
}