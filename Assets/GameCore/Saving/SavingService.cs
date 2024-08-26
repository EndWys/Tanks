using System.IO;
using UnityEngine;
using VContainer;

namespace Assets.GameCore.Saving
{
    public class SavingService
    {
        private const string GAME_SAVE_FILE = "/GameSave.json";

        private string _path => Path.Combine(Application.streamingAssetsPath, GAME_SAVE_FILE);

        [Inject]
        public SavingService()
        {

        }

        public void Save<T>(T entitiy) where T : class
        {
            File.WriteAllText(Application.streamingAssetsPath + GAME_SAVE_FILE, JsonUtility.ToJson(entitiy));
        }

        public T Load<T>() where T : class
        {
            string value = File.ReadAllText(Application.streamingAssetsPath + GAME_SAVE_FILE);
            if (string.IsNullOrEmpty(value)) return null;

            return JsonUtility.FromJson<T>(value);
        }
    }
}