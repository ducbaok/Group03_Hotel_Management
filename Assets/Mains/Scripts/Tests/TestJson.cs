using Sirenix.OdinInspector;
using UnityEngine;
using YNL.Utilities.Extensions;

namespace YNL.Checkotel
{
    public class TestJson : MonoBehaviour
    {
        [SerializeField] private DatabaseContainerSO _database;
        [SerializeField] private string _path;

        [Button]
        public void Test()
        {
            _database.Hotels.SaveNewtonJson(_path);
        }
    }
}