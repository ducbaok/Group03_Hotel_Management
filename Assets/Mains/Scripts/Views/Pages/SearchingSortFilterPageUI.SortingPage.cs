using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public partial class SearchingSortFilterPageUI
    {
        public class SortingPage : IInitializable, IResetable
        {
            public SortingSelectionType SortingType;

            private VisualElement _root;
            private VisualElement _sortingPage;
            private VisualElement _closeButton;
            private VisualElement _applyButton;
            private VisualElement _sortSelectionArea;

            private List<SortingSelectionItemUI> _sortingItemUI = new();

            public SortingPage(VisualElement root)
            {
                SortingSelectionItemUI.OnSelected += OnSortingTypeSelected;

                _root = root;

                _sortingPage = _root.Q("SortingPage");

                _closeButton = _sortingPage.Q("LabelField").Q("CloseButton");
                _closeButton.RegisterCallback<PointerDownEvent>(OnClicked_CloseButton);

                _applyButton = _sortingPage.Q("Toolbar").Q("ApplyButton");
                _applyButton.RegisterCallback<PointerDownEvent>(OnClicked_ApplyButton);

                _sortSelectionArea = _sortingPage.Q("SortSelectionArea");
                _sortSelectionArea.Clear();

                Initialize();
                Reset();
            }
            ~SortingPage()
            {
                SortingSelectionItemUI.OnSelected -= OnSortingTypeSelected;
            }

            public void Initialize()
            {
                var sortingTypes = Enum.GetValues(typeof(SortingSelectionType)) as SortingSelectionType[];

                for (byte i = 0; i < sortingTypes.Length; i++)
                {
                    var item = new SortingSelectionItemUI(sortingTypes[i]);
                    if (i == sortingTypes.Length - 1) item.SetAsLastItem();

                    _sortingItemUI.Add(item);
                    _sortSelectionArea.Add(item);
                }
            }

            public void Reset()
            {
                _sortingItemUI[0].OnClicked__Toggle();
            }

            private void OnClicked_CloseButton(PointerDownEvent evt)
            {
            }

            private void OnClicked_ApplyButton(PointerDownEvent evt)
            {
            }

            private void OnSortingTypeSelected(SortingSelectionType type)
            {
                SortingType = type;
            }
        }
    }
}