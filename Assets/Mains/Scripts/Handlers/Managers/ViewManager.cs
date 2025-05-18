using UnityEngine;
using YNL.Utilities.Addons;

namespace YNL.Checkotel
{
	public class ViewManager : MonoBehaviour
	{
		public ViewType CurrentViewType;
		public byte CurrentViewPage;
        public bool IsAbleToMovePage = true;

		public SerializableDictionary<ViewType, ViewGroup> Groups = new();

        public void Awake()
        {
            Marker.OnViewPageSwitched += OnViewPageSwitched;
        }

        private void OnDestroy()
        {
            Marker.OnViewPageSwitched -= OnViewPageSwitched;
        }

        private void OnViewPageSwitched(ViewType type, byte page, bool hidePreviousPage)
        {
            if (!IsAbleToMovePage) return;

            var currentGroup = Groups[CurrentViewType];
            if (hidePreviousPage)
            {
                currentGroup.Pages[CurrentViewPage].DisplayView(false);
            }

            CurrentViewType = type;
            CurrentViewPage = page;

            currentGroup = Groups[CurrentViewType];
            currentGroup.Pages[CurrentViewPage].DisplayView(true);
        }
    }

    public enum ViewType : byte
    {
        SigningView, MainView, SearchView, InformationView
    }

    [System.Serializable]
    public class ViewGroup
    {
        public GameObject View;
        public SerializableDictionary<byte, ViewPageUI> Pages;
    }


    public static class ViewKey
    {
        public const byte SingingViewSignUpPage = 0;
        public const byte SingingViewSignInPage = 1;

        public const byte MainViewHomePage = 0;
        public const byte MainViewAccountPage = 1;

        public const byte SearchViewMainPage = 0;
        public const byte SearchViewResultPage = 1;
    }
}