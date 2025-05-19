using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using YNL.Utilities.Addons;

namespace YNL.Checkotel
{
    [CreateAssetMenu(fileName = "RuntimePropertiesSO", menuName = "YNL - Checkotel/RuntimePropertiesSO")]
    public class RuntimePropertiesSO : ScriptableObject
    {
        public UID AccountID;
        public List<string> SearchingAddressHistory = new();
    }
}