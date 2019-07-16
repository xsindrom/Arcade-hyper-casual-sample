using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    public interface IStorageHandler: IEventHandler
    {
        void OnLoad(StorageData data);
        void OnSave(StorageData data);
    }

    public class StorageController : IDisposable
    {
        public StorageData StorageData { get; set; }

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
            EventManager.Call<IStorageHandler>(x => x.OnLoad(StorageData));
        }

        public void Save()
        {
            EventManager.Call<IStorageHandler>(x => x.OnSave(StorageData));

            var json = JsonUtility.ToJson(StorageData);
            File.WriteAllText($"{Application.persistentDataPath}/data.json", json);
        }

        public void Dispose()
        {
            StorageData = null;
        }
    }
}