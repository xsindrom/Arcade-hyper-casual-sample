using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currency;
using Storage;

public class GameController : MonoSingleton<GameController>
{
    public CurrencyController CurrencyController { get; private set; }
    public StorageController StorageController { get; private set; }

    public override void Init()
    {
        base.Init();
        StorageController = new StorageController();
        CurrencyController = new CurrencyController();

        StorageController.Load();
    }

    protected override void OnApplicationPause(bool pause)
    {
        base.OnApplicationPause(pause);
        if (pause)
            StorageController.Save();
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        StorageController.Save();
    }

    protected override void OnDestroy()
    {
        CurrencyController?.Dispose();
        StorageController?.Dispose();
        base.OnDestroy();
    }
}
