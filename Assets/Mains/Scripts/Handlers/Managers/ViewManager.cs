using UnityEngine;
using YNL.Utilities.Addons;

namespace YNL.Checkotel
{
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

	public class ViewManager : MonoBehaviour
	{
		public ViewType CurrentViewType;
		public byte CurrentViewPage;

		public SerializableDictionary<ViewType, ViewGroup> Groups = new();
	}
}