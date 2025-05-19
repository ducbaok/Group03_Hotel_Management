using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public class MainViewNavigationPageUI : ViewPageUI
    {
        private VisualElement _navigationBar;

        protected override void Collect()
        {
            _navigationBar = Root.Q("NavigationBar");
        }

        protected override void Initialize()
        {
            _navigationBar.Clear();
            _navigationBar.Add(new HomeNavigationButton(Main.Resources.Icons["Home"], "Home", true, HomeNavigationType.Home));
            _navigationBar.Add(new HomeNavigationButton(Main.Resources.Icons["Star"], "Favorite", false, HomeNavigationType.Favorite));
            _navigationBar.Add(new HomeNavigationButton(Main.Resources.Icons["Suggestion"], "Suggestion", false, HomeNavigationType.Suggestion));
            _navigationBar.Add(new HomeNavigationButton(Main.Resources.Icons["Account"], "Account", false, HomeNavigationType.Account));
        }
    }
}