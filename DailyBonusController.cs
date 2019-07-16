using Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusController : IStorageHandler, IDisposable
{
    private DailyBonusSettings settings;
    private int startTime;

    public DailyBonusSettings Settings
    {
        get
        {
            if (!settings)
            {
                settings = ResourceManager.GetResource<DailyBonusSettings>(GameConstants.PATH_DAILY_BONUS_SETTINGS);
            }
            return settings;
        }
    }

    public DailyBonusController()
    {
        EventManager.AddHandler(this);
    }

    public void OnGetBonus()
    {
        startTime = DateTime.UtcNow.ToUnixTime();
    }

    public bool IsDailyBonusReady()
    {
        return (DateTime.UtcNow.ToUnixTime() - startTime) > Settings.RepeatTimeInSeconds;
    }

    public void OnLoad(StorageData data)
    {
        startTime = data.UserData.BonusStartTime;
    }

    public void OnSave(StorageData data)
    {
        data.UserData.BonusStartTime = startTime;
    }

    public void Dispose()
    {
        Resources.UnloadAsset(settings);
        EventManager.RemoveHandler(this);
    }
}
