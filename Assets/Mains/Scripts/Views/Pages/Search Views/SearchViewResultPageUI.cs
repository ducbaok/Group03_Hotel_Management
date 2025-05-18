using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class SearchViewResultPageUI : ViewPageUI
    {
        [SerializeField] private SearchViewSortPageUI _sortPage;
        [SerializeField] private SearchViewFilterPageUI _filterPage;

        private VisualElement _searchBar;
        private Label _searchText;
        private ScrollView _resultScroll;
        private ListView _resultList;
        private VisualElement _sortButton;
        private VisualElement _filterButton;

        protected override void Collect()
        {
            var resultPage = Root.Q("SearchingResultPage");

            _searchBar = resultPage.Q("SearchBar");
            _searchBar.RegisterCallback<PointerDownEvent>(OnClicked_SearchBar);

            _searchText = _searchBar.Q("SearchField").Q("SearchText") as Label;

            _resultScroll = Root.Q("ResultScroll") as ScrollView;
            resultPage.Remove(_resultScroll);

            _resultList = new ListView().SetFlexGrow(1).SetMarginTop(50);
            resultPage.Add(_resultList);

            _sortButton = resultPage.Q("ResultField").Q("SortingButton");
            _sortButton.RegisterCallback<PointerDownEvent>(OnClicked_SortButton);

            _filterButton = resultPage.Q("ResultField").Q("FilteringButton");
            _filterButton.RegisterCallback<PointerDownEvent>(OnClicked_FilterButton);
        }

        protected override void Refresh()
        {
            bool[] list = new bool[10];

            _resultList.dataSource = list;
        }

        private void OnClicked_SearchBar(PointerDownEvent evt)
        {
            Marker.OnViewPageSwitched?.Invoke(ViewType.SearchView, ViewKey.SearchViewMainPage, true);
        }

        private void OnClicked_SortButton(PointerDownEvent evt)
        {
            _sortPage.OnPageOpened(true);
        }

        private void OnClicked_FilterButton(PointerDownEvent evt)
        {
            _filterPage.OnPageOpened(true);
        }
    }
}