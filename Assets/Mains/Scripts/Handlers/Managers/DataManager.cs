using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
namespace YNL.Checkotel
{
    public class DataManager : MonoBehaviour
    {
        public static string HotelImageURL;
        public static string RoomImageURL;

        public bool EnableInitializeDatabase = true;

        private DatabaseContainerSO _database => Main.Database;

        [SerializeField] private string _hotelDatabaseURL;
        [SerializeField] private string _roomDatabaseURL;
        [SerializeField] private string _feedbackDatabaseURL;
        [SerializeField] private string _configDatabaseURL;

        private void Start()
        {
            Main.Runtime.Reset();

            InitializeDatabases().Forget();
        }

        private async UniTaskVoid InitializeDatabases()
        {
            if (EnableInitializeDatabase)
            {
                await InitializeConfigDatabase();
                await InitializeHotelDatabase();
                await InitializeRoomDatabase();
                await InitializeFeedbackDatabase();
            }

            await UniTask.Delay(100);

            Marker.OnDatabaseSerializationDone?.Invoke();
            Main.IsSystemStarted = true;
        }

        private async UniTask InitializeConfigDatabase()
        {
            var content = await _configDatabaseURL.GetRawDatabaseAsync();

            string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            HotelImageURL = lines[0];
            RoomImageURL = lines[1];
        }

        private async UniTask InitializeHotelDatabase()
        {
            var content = await _hotelDatabaseURL.GetRawDatabaseAsync();

            _database.Hotels.Clear();

            string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int l = 1; l < lines.Length; l++)
            {
                var fields = lines[l].SplitCSV();

                if (string.IsNullOrEmpty(fields[1])) break;

                _database.SerializeHotelDatabase(fields);
            }
        }

        private async UniTask InitializeRoomDatabase()
        {
            var content = await _roomDatabaseURL.GetRawDatabaseAsync();

            _database.Rooms.Clear();

            string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int l = 1; l < lines.Length; l++)
            {
                var fields = lines[l].SplitCSV();

                if (string.IsNullOrEmpty(fields[1])) break;

                _database.SerializeRoomDatabase(fields);
            }
        }

        private async UniTask InitializeFeedbackDatabase()
        {
            var content = await _feedbackDatabaseURL.GetRawDatabaseAsync();

            _database.Feedbacks.Clear();

            string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int l = 1; l < lines.Length; l++)
            {
                var fields = lines[l].SplitCSV();

                if (string.IsNullOrEmpty(fields[1])) break;

                _database.SerializeFeedbackDatabase(fields);
            }
        }
    }
}