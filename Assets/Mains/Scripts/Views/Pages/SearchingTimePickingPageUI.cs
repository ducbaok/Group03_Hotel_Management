using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public class SearchingTimePickingPageUI : MonoBehaviour, IInitializable
    {
        private VisualElement _root;

        private VisualElement _closeButton;
        private Label _checkInTime;
        private Label _checkOutTime;
        private Label _durationTime;
        private VisualElement _cancelButton;
        private VisualElement _applyButton;

        private void Awake()
        {
            Marker.OnSystemStart += Initialize;
        }

        private void OnDestroy()
        {
            Marker.OnSystemStart -= Initialize;
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
        }
    }
}