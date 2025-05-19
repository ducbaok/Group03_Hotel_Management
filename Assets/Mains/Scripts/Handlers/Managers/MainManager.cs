using YNL.Checkotel;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.Addons;
using YNL.Utilities.Patterns;

namespace YNL.Checkotel
{
    public static class Main
    {
        public static MainManager Manager => MainManager.Instance;
        public static MainResources Resources => Manager.Resources;
        public static DatabaseContainerSO Database => Manager.Database;
        public static RuntimePropertiesSO Runtime => Manager.Runtime;
        public static ViewManager View => Manager.ViewManager;
	}

    public class MainManager : Singleton<MainManager>
    {
        public MainResources Resources = new();
        public DatabaseContainerSO Database;
        public RuntimePropertiesSO Runtime;

        public ViewManager ViewManager;

        protected override void Awake()
        {
            base.Awake();

            Application.targetFrameRate = 120;
        }

        private void Start()
        {
            Marker.OnSystemStart?.Invoke();
        }
    }

    [System.Serializable]
    public class MainResources
    {
        public SerializableDictionary<string, StyleSheet> Styles = new();
        public SerializableDictionary<string, Texture2D> Icons = new();
    }
}