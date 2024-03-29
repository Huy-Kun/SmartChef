using Dacodelaac.Utils;
using SurvivorRoguelike;
using UnityEngine;

namespace Dacodelaac.DataStorage
{
    public static class GameData
    {
        private static DataStorage storage;
        public static DataStorage Storage => storage ?? (storage = new DataStorage("data.dat"));

        public static T Get<T>(string key, T defaultValue = default)
        {
            return Storage.Get<T>(key, defaultValue);
        }

        public static void Set<T>(string key, T data)
        {
            Storage[key] = data;
        }

        public static void Remove(string key) => Storage.Remove(key);

        public static bool Contains(string key) => Storage.ContainsKey(key);

        public static void Load(IDataPersistent dataPersistent, bool root = false)
        {
            Storage.Load(dataPersistent, root);
        }

        public static void Store(IDataPersistent dataPersistent, bool root = false)
        {
            Storage.Store(dataPersistent, root);
        }

        public static void Save()
        {
            Storage.Save();
        }

        public static void Clear()
        {
            storage = null;
        }
    }
}