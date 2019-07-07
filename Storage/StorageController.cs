using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    public class StorageController : IDisposable
    {
        public StorageData StorageData { get; set; }

        public event Action<StorageData> OnLoad;
        public event Action<StorageData> OnSave;

        public void Load()
        {
            var path = $"{Application.persistentDataPath}/data.json";
            if (!File.Exists(path))
            {
                StorageData = new StorageData();
            }
            else
            {
                var json = File.ReadAllText(path);
                StorageData = JsonUtility.FromJson<StorageData>(json);
            }
            OnLoad?.Invoke(StorageData);
        }

        public void Save()
        {
            OnSave?.Invoke(StorageData);

            var json = JsonUtility.ToJson(StorageData);
            File.WriteAllText($"{Application.persistentDataPath}/data.json", json);
        }

        public void Dispose()
        {
            StorageData = null;
            OnLoad = null;
            OnSave = null;
        }
    }
}