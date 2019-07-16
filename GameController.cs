using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currencies;
using Storage;
using Units;
using Battle;
using Rewards;
using Network.GameServer;
using Cards;
using SpecialEvents;
using Utilities.Localization;
using System;

public class GameController : MonoSingleton<GameController>
{
    #region Controllers
    private CurrencyController currencyController;
    private StorageController storageController;
    private ChestsController chestsController;
    private CardDataController cardDataController;
    private SpecialEventsController specialEventsController;
    private ArenaController arenaController;
    private UserExperienceController userExperienceController;

    public CurrencyController CurrencyController
    {
        get { return currencyController; }
    }

    public StorageController StorageController
    {
        get { return storageController; }
    }

    public ChestsController ChestsController
    {
        get { return chestsController; }
    }

    public CardDataController CardDataController
    {
        get { return cardDataController; }
    }

    public SpecialEventsController SpecialEventsController
    {
        get { return specialEventsController; }
    }

    public UserExperienceController UserExperienceController
    {
        get { return userExperienceController; }
    }

    public ArenaController ArenaController
    {
        get { return arenaController; }
    }
    #endregion

    private UnitDatabase unitDatabase;
    public UnitDatabase UnitDatabase
    {
        get
        {
            if (!unitDatabase)
            {
                unitDatabase = AssetBundlesLoader.Instance.GetAsset<UnitDatabase>(Constants.UNITS_BUNDLE, Constants.ASSET_UNITS_DATABASE);
            }
            return unitDatabase;
        }
    }

    private RewardsStorage rewardsStorage;
    public RewardsStorage RewardsStorage
    {
        get
        {
            if (!rewardsStorage)
            {
                rewardsStorage = AssetBundlesLoader.Instance.GetAsset<RewardsStorage>(Constants.SETTINGS_BUNDLE, Constants.ASSET_REWARDS_STORAGE);
            }
            return rewardsStorage;
        }
    }

    private SpeedStatRangeContainer rangeContainer;
    public SpeedStatRangeContainer RangeContainer
    {
        get
        {
            if (!rangeContainer)
            {
                rangeContainer = AssetBundlesLoader.Instance.GetAsset<SpeedStatRangeContainer>(Constants.UNITS_BUNDLE, Constants.ASSET_SPEED_STAT_RANGES);
            }
            return rangeContainer;
        }
    }

    protected override void Init()
    {
        base.Init();
#if UNITY_EDITOR || UNITY_STANDALONE
        //TEST
        Screen.SetResolution(304, 540, false);
        ///
#endif

        Application.targetFrameRate = 60;

        storageController = new StorageController();

        GooglePlayServices.Activate();
        FacebookSDK.Init();

        GameServer.Instance.Login(OnLogin);
    }

    protected override void OnDestroy()
    {
        
        chestsController?.Dispose();
        cardDataController?.Dispose();
        specialEventsController?.Dispose();
        arenaController?.Dispose();
        userExperienceController?.Dispose();
        storageController?.Dispose();
        currencyController?.Dispose();

        chestsController = null;
        cardDataController = null;
        specialEventsController = null;
        arenaController = null;
        userExperienceController = null;
        storageController = null;

        currencyController = null;
        AssetBundlesLoader.UnSubscribeOnBundlesLoaded(this);
        base.OnDestroy();
    }

    private void OnLogin(GameServerLoginResponce loginResponse)
    {
        if (loginResponse.status == Status.OK)
        {
            GameServer.Instance.GetSaveData(OnGetSaveData);
        }
        else
        {
            InitBySaveData(loginResponse);
        }
    }

    private void OnGetSaveData(GameServerGetSaveResponse response, SaveData saveData)
    {
        if (response.status == Status.OK)
        {
            if (saveData != null)
            {
                storageController.Data = saveData;
            }
            else
            {
                storageController.Load();
                GameServer.Instance.UpdateSaveData(InitBySaveData);
                return;
            }
        }
        InitBySaveData(response);
    }

    private void InitBySaveData(GameServerBaseResponse response)
    {
        AssetBundlesLoader.SubscribeOrGetOnBundlesLoaded(this,OnSettingsBundleLoaded, Constants.SETTINGS_BUNDLE);
    }

    private void OnSettingsBundleLoaded(string[] bundles)
    {
        Teams.TeamsController.CreateInstance(true);

        currencyController = new CurrencyController();
        chestsController = new ChestsController();
        cardDataController = new CardDataController();
        specialEventsController = new SpecialEventsController();
        userExperienceController = new UserExperienceController();
        arenaController = new ArenaController();

        storageController.Load();

        Language.Instance.LangCode = (LangCode)Enum.Parse(typeof(LangCode), storageController.Data.UserData.Language.Value);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            storageController.Save();
            GameServer.Instance.UpdateSaveData(InitBySaveData);
        }
    }
}
