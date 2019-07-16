using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currency;
using Storage;
using Shop;
using Ads;
public class GameController : MonoSingleton<GameController>
{
    public CurrencyController CurrencyController { get; private set; }
    public StorageController StorageController { get; private set; }
    public ShopController ShopController { get; private set; }
    public AdsController AdsController { get; private set; }
    public DailyBonusController DailyBonusController { get; private set; }
    public override void Init()
    {
        base.Init();
        StorageController = new StorageController();
        CurrencyController = new CurrencyController();
        ShopController = new ShopController();
        AdsController = new AdsController();
        DailyBonusController = new DailyBonusController();
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
        ShopController?.Dispose();
        AdsController?.Dispose();
        DailyBonusController?.Dispose();
        StorageController?.Dispose();
        base.OnDestroy();
    }
}
