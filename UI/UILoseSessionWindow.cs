using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Session;
using TMPro;
using Currency;
public class UILoseSessionWindow : UIBaseWindow, ISessionHandler
{
    [SerializeField]
    private UICurrencyItem coins;
    [SerializeField]
    private TMP_Text scoresText;
    [SerializeField]
    private TMP_Text maxScoresText;
    [SerializeField]
    private Button adsContinueButton;
    [SerializeField]
    private Button currencyContinueButton;
    [SerializeField]
    private UICurrencyItem currencyContinueItem;

    private bool continueAdsWatched = false;
    private int currentContinuePriceIndex = 0;
    private ContinueSessionSettings settings;
    public ContinueSessionSettings Settings
    {
        get
        {
            if (!settings)
            {
                settings = ResourceManager.GetResource<ContinueSessionSettings>(GameConstants.PATH_CONTINUE_SESSION_SETTINGS);
            }
            return settings;
        }
    }

    public override void Init()
    {
        base.Init();
        EventManager.AddHandler(this);
    }

    public void OnContinueSessionByAdsButtonClick()
    {
        if(!continueAdsWatched)
        {
            continueAdsWatched = true;
            GameController.Instance.AdsController.Show(Settings.AdsPlacement);
        }
    }

    public void OnContinueSessionByCurrencyButtonClick()
    {
        var currentPrice = Settings.Prices[Mathf.Clamp(currentContinuePriceIndex, 0, Settings.Prices.Count)];
        if (GameController.Instance.CurrencyController.TrySubstract(currentPrice.currencyType, currentPrice.currencyAmount))
        {
            SessionController.Instance.ContinueSession();
            currentContinuePriceIndex++;
        }
    }

    public void OnMainMenuButtonClick()
    {
        CloseWindow();
        currentContinuePriceIndex = 0;
        continueAdsWatched = false;
        var mainMenu = UIMainController.Instance.GetWindow<UIMainMenu>(UIConstants.WINDOW_MAIN_MENU);
        mainMenu?.OpenWindow();
    }

    public override void OpenWindow()
    {
        coins.Source = new CurrencyItem()
        {
            currencyType = CurrencyType.Coins,
            currencyAmount = GameController.Instance.CurrencyController.GetCurrency(CurrencyType.Coins)
        };
        scoresText.text = SessionController.Instance.Scores.ToString();
        maxScoresText.text = $"High: {GameController.Instance.StorageController.StorageData.SessionsData.HighScore}";

        adsContinueButton.gameObject.SetActive(!continueAdsWatched);
        currencyContinueItem.Icon.sprite = null;
        currencyContinueItem.Source = Settings.Prices[Mathf.Clamp(currentContinuePriceIndex, 0, Settings.Prices.Count)];
        base.OpenWindow();
    }

    public void StartSession()
    {

    }

    public void ContinueSession()
    {
        CloseWindow();
    }

    public void LoseSession()
    {

    }

    public void WinSession()
    {

    }
}
