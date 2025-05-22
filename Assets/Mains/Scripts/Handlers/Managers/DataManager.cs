using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using YNL.Utilities.Extensions;

namespace YNL.Checkotel
{
    public class DataManager : MonoBehaviour
    {
        private DatabaseContainerSO _database => Main.Database;

        [SerializeField] private string _hotelDatabaseURL;

        private void Start()
        {
            InitializeDatabases().Forget();
        }

        private async UniTaskVoid InitializeDatabases()
        {
            await InitializeHotelDatabase();
            await InitializeRoomDatabase();

            Marker.OnDatabaseSerializationDone?.Invoke();
        }

        private async UniTask InitializeHotelDatabase()
        {
            var content = await _hotelDatabaseURL.GetRawDatabaseAsync();

            _database.Hotels.Clear();

            string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int l = 1; l < lines.Length; l++)
            {
                _database.SerializeHotelDatabse(lines[l].SplitCSV());
            }
        }

        private async UniTask InitializeRoomDatabase()
        {
            await UniTask.Delay(1000);
        }
    }
}