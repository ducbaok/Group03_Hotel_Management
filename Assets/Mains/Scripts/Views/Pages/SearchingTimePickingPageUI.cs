using System;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.Extensions;
using static YNL.Checkotel.SearchingTimePickingPageUI;

namespace YNL.Checkotel
{
    public partial class SearchingTimePickingPageUI : MonoBehaviour, IInitializable
    {
        private VisualElement _root;

        private VisualElement _closeButton;
        private Label _checkInTime;
        private Label _checkOutTime;
        private Label _durationTime;
        private VisualElement _cancelButton;
        private VisualElement _applyButton;

        private HourlyPage _hourlyPage;

        private void Awake()
        {
            Marker.OnSystemStart += Initialize;
            Marker.OnTimeRangeChanged += OnTimeRangeChanged;
        }

        private void OnDestroy()
        {
            Marker.OnSystemStart -= Initialize;
            Marker.OnTimeRangeChanged -= OnTimeRangeChanged;
        }

        public void Initialize()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            _closeButton = _root.Q("LabelField").Q("CloseButton");

            var resultField = _root.Q("ResultField");

            _checkInTime = resultField.Q("CheckInField").Q("Time") as Label;
            _checkOutTime = resultField.Q("CheckOutField").Q("Time") as Label;
            _durationTime = resultField.Q("DurationField").Q("Time") as Label;

            _cancelButton = _root.Q("ToolBar").Q("CancelButton");
            _applyButton = _root.Q("ToolBar").Q("ApplyButton");

            _hourlyPage = new HourlyPage(_root);
        }

        private void OnTimeRangeChanged(DateTime checkInDate, byte duration)
        {
            _checkInTime.text = checkInDate.ToString("dd/MM, HH:mm");
            _checkOutTime.text = checkInDate.AddHours(duration).ToString("dd/MM, HH:mm");
            _durationTime.text = duration == 1 ? "1 hour" : $"{duration} hours";
        }
    }
}