using System;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public partial class SearchViewTimeRangePageUI : ViewPageUI
    {
        public (DateTime CheckInTime, byte Duration) TimeRange;

        private VisualElement _background;
        private VisualElement _page;
        private VisualElement _closeButton;
        private Label _checkInTime;
        private Label _checkOutTime;
        private Label _durationTime;
        private VisualElement _cancelButton;
        private VisualElement _applyButton;

        private HourlyPage _hourlyPage;

        protected override void VirtualAwake()
        {
            Marker.OnTimeRangeChanged += OnTimeRangeChanged;
        }

        private void OnDestroy()
        {
            Marker.OnTimeRangeChanged -= OnTimeRangeChanged;
        }

        protected override void Collect()
        {
            _background = Root.Q("ScreenBackground");
            _background.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);
            _page = Root.Q("TimePickingPage");

            _closeButton = Root.Q("LabelField");
            _closeButton.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);

            var resultField = Root.Q("ResultField");

            _checkInTime = resultField.Q("CheckInField").Q("Time") as Label;
            _checkOutTime = resultField.Q("CheckOutField").Q("Time") as Label;
            _durationTime = resultField.Q("DurationField").Q("Time") as Label;

            _cancelButton = Root.Q("ToolBar").Q("CancelButton");
            _cancelButton.RegisterCallback<PointerDownEvent>(OnClicked_CancelButton);

            _applyButton = Root.Q("ToolBar").Q("ApplyButton");
            _applyButton.RegisterCallback<PointerDownEvent>(OnClicked_ApplyButton);

            _hourlyPage = new HourlyPage(Root);
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

        private void OnClicked_CancelButton(PointerDownEvent evt)
        {

        }

        private void OnClicked_ApplyButton(PointerDownEvent evt)
        {

        }

        private void OnTimeRangeChanged(DateTime checkInTime, byte duration)
        {
            _checkInTime.text = checkInTime.ToString("dd/MM, HH:mm");
            _checkOutTime.text = checkInTime.AddHours(duration).ToString("dd/MM, HH:mm");
            _durationTime.text = duration == 1 ? "1 hour" : $"{duration} hours";

            TimeRange = (checkInTime, duration);
        }
    }
}