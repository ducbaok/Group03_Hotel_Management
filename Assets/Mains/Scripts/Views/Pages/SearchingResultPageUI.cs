using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class SearchingResultPageUI : MonoBehaviour, IInitializable
    {
        private VisualElement _root;

        private VisualElement _resultPage;
        private ScrollView _resultScroll;
        private ListView _resultList;

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

            _resultPage = _root.Q("SearchingResultPage");

            _resultScroll = _root.Q("ResultScroll") as ScrollView;
            _resultPage.Remove(_resultScroll);

            _resultList = new ListView().SetFlexGrow(1).SetMarginTop(50);
            _resultList.Add(_resultScroll);

            Reset();
        }

        public void Reset()
        {
            bool[] list = new bool[10];

            _resultList.dataSource = list;
        }
    }
}