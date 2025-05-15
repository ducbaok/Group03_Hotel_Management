using UnityEngine;
using YNL.Utilities.Addons;

namespace YNL.Checkotel
{
	public class ViewManager : MonoBehaviour
	{
		public ViewType CurrentViewType;
		public byte CurrentViewPage;

		public SerializableDictionary<ViewType, ViewGroup> Groups = new();

        private void Awake()
        {
            Marker.OnViewPageSwitched += OnViewPageSwitched;
        }

        private void OnDestroy()
        {
            Marker.OnViewPageSwitched -= OnViewPageSwitched;
        }

        private void OnViewPageSwitched(ViewType type, byte page)
        {
            var currentGroup = Groups[CurrentViewType];
            currentGroup.View.SetActive(false);
            currentGroup.Pages[CurrentViewPage].gameObject.SetActive(false);

            var nextGroup = Groups[type];
            nextGroup.View.SetActive(true);
            nextGroup.Pages[page].gameObject.SetActive(true);
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
    }
}