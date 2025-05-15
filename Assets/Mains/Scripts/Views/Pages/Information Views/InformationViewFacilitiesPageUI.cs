using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public partial class InformationViewFacilitiesPageUI : ViewPageUI, ICollectible, IInitializable
    {
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


            Initialize();
        }

        public void Initialize()
        {
        }
    }
}