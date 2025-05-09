using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public partial class SearchPageUI : MonoBehaviour, IInitializable
    {
        private VisualElement _root;

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
        }
    }
}