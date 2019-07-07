using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private int coinsGained;

    public override void Init()
    {
        base.Init();
        EventManager.AddHandler(this);
        GameController.Instance.CurrencyController.OnCurrencyChanged += OnCurrencyChanged;
    }

    private void OnCurrencyChanged(CurrencyType type, int prevCount, int newCount)
    {
        if (newCount > prevCount && type == CurrencyType.Coins)
        {
            coinsGained++;
        }
    }

    public void OnSecondLifeButtonClick()
    {

    }

    public void OnMainMenuButtonClick()
    {
        CloseWindow();
        var mainMenu = UIMainController.Instance.GetWindow<UIMainMenu>(UIConstants.WINDOW_MAIN_MENU);
        mainMenu?.OpenWindow();
    }

    public override void OpenWindow()
    {
        coins.Source = new CurrencyItem()
        {
            currencyType = CurrencyType.Coins,
            currencyAmount = coinsGained
        };
        scoresText.text = SessionController.Instance.Scores.ToString();
        maxScoresText.text = $"High: {GameController.Instance.StorageController.StorageData.SessionsData.HighScore}";
        coinsGained = 0;
        base.OpenWindow();
    }

    public void LoseSession()
    {
        OpenWindow();
    }

    public void StartSession()
    {
    }

    public void WinSession()
    {
    }
}
