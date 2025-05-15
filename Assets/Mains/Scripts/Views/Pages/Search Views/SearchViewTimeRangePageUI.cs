using System;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public partial class SearchViewTimeRangePageUI : ViewPageUI, ICollectible
    {
        public (DateTime CheckInTime, byte Duration) TimeRange;

        private VisualElement _closeButton;
        private Label _checkInTime;
        private Label _checkOutTime;
        private Label _durationTime;
        private VisualElement _cancelButton;
        private VisualElement _applyButton;

        private HourlyPage _hourlyPage;

        protected override void VirtualAwake()
        {
            Marker.OnSystemStart += Collect;
            Marker.OnTimeRangeChanged += OnTimeRangeChanged;
        }

        private void OnDestroy()
        {
            Marker.OnSystemStart -= Collect;
            Marker.OnTimeRangeChanged -= OnTimeRangeChanged;
        }

        public void Collect()
        {
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

        private void OnClicked_CloseButton(PointerDownEvent evt)
        {
            Root.SetDisplay(DisplayStyle.None);
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