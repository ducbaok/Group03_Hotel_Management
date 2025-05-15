using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class SearchViewResultPageUI : ViewPageUI, ICollectible, IRefreshable
    {
        private VisualElement _resultPage;
        private ScrollView _resultScroll;
        private ListView _resultList;

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


            _resultPage = Root.Q("SearchingResultPage");

            _resultScroll = Root.Q("ResultScroll") as ScrollView;
            _resultPage.Remove(_resultScroll);

            _resultList = new ListView().SetFlexGrow(1).SetMarginTop(50);
            _resultList.hierarchy.Add(_resultScroll);

            Refresh();
        }

        public void Refresh()
        {
            bool[] list = new bool[10];

            _resultList.dataSource = list;
        }
    }
}