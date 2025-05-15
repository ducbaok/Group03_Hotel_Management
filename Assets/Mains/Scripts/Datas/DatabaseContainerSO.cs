using System.Collections.Generic;
using UnityEngine;

namespace YNL.Checkotel
{
    [CreateAssetMenu(fileName = "DatabaseContainerSO", menuName = "YNL - Checkotel/DatabaseContainerSO")]
    public class DatabaseContainerSO : ScriptableObject
    {
        public List<Account> Accounts = new();
    }
}