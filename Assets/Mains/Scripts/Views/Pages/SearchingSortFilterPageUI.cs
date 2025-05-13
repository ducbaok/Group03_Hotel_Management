using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public partial class SearchingSortFilterPageUI : MonoBehaviour, IInitializable
    {
        private VisualElement _root;

        private SortingPage _sortingPage;
        private FilteringPage _filteringPage;

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

            _sortingPage = new SortingPage(_root);
            _filteringPage = new FilteringPage(_root);
        }
    }
}