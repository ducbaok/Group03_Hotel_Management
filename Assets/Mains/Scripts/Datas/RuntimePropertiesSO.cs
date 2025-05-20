using System.Collections.Generic;
using UnityEngine;
using YNL.Utilities.Addons;

namespace YNL.Checkotel
{
    [System.Serializable]
    public class LikedFeedback
    {
        public List<UID> Feedbacks = new();
    }

    [CreateAssetMenu(fileName = "RuntimePropertiesSO", menuName = "YNL - Checkotel/RuntimePropertiesSO")]
    public class RuntimePropertiesSO : ScriptableObject
    {
        public UID AccountID;
        public List<string> SearchingAddressHistory = new();
        public SerializableDictionary<UID, LikedFeedback> LikedFeedbacks = new();
    }
}