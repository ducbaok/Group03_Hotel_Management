using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public abstract class ViewPageUI : MonoBehaviour
    {
        [SerializeField] private bool _hideOnAwake = true;
        [SerializeField] private ViewPageUI _dependingView;
        public VisualElement Root;

        private void Awake()
        {
            Root = GetComponent<UIDocument>().rootVisualElement;

            VirtualAwake();

            Root.SetDisplay(_hideOnAwake ? DisplayStyle.None : DisplayStyle.Flex);
        }

        public void DisplayView(bool display)
        {
            _dependingView?.Root.SetDisplay(display ? DisplayStyle.Flex : DisplayStyle.None);
            Root.SetDisplay(display ? DisplayStyle.Flex : DisplayStyle.None);
        }

        protected virtual void VirtualAwake() { }
    }
}