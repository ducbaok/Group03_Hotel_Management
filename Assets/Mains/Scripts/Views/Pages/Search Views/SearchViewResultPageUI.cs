using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.Extensions;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class SearchViewResultPageUI : ViewPageUI, ICollectible, IRefreshable
    {
        [SerializeField] private SearchViewSortPageUI _sortPage;
        [SerializeField] private SearchViewFilterPageUI _filterPage;

        private Label _searchText;
        private ScrollView _resultScroll;
        private ListView _resultList;
        private VisualElement _sortButton;
        private VisualElement _filterButton;

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
            var resultPage = Root.Q("SearchingResultPage");

            _searchText = resultPage.Q("SearchBar").Q("SearchField").Q("SearchText") as Label;

            _resultScroll = Root.Q("ResultScroll") as ScrollView;
            resultPage.Remove(_resultScroll);

            _resultList = new ListView().SetFlexGrow(1).SetMarginTop(50);
            resultPage.Add(_resultList);

            _sortButton = resultPage.Q("ResultField").Q("SortingButton");
            _sortButton.RegisterCallback<PointerDownEvent>(OnClicked_SortButton);

            _filterButton = resultPage.Q("ResultField").Q("FilteringButton");
            _filterButton.RegisterCallback<PointerDownEvent>(OnClicked_FilterButton);

            Refresh();
        }

        public void Refresh()
        {
            bool[] list = new bool[10];

            _resultList.dataSource = list;
        }

        private void OnClicked_SortButton(PointerDownEvent evt)
        {
            _sortPage.Root.SetTranslate(0, 0, true);
        }

        private void OnClicked_FilterButton(PointerDownEvent evt)
        {
            _filterPage.Root.SetTranslate(0, 0, true);
        }
    }
}