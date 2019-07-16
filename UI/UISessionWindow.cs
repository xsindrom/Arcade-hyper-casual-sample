using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Session;
using TMPro;
using Currency;
public class UISessionWindow : UIBaseWindow, ISessionHandler
{
    [SerializeField]
    private TMP_Text scoresText;
    [SerializeField]
    private UICurrencyItem coinsItem;

    private int prevCoinsGained;
    private int coinsGained;

    public override void Init()
    {
        base.Init();
        SessionController.Instance.OnScoresChanged += OnScoresChanged;
        GameController.Instance.CurrencyController.OnCurrencyChanged += OnCurrencyChanged;
        EventManager.AddHandler(this);
    }

    private void OnCurrencyChanged(CurrencyType currencyType, int prevCoins, int newCoins)
    {
        if (currencyType != CurrencyType.Coins)
            return;

        coinsGained += newCoins - prevCoins;
        coinsItem.Source = new CurrencyItem()
        {
            currencyType = CurrencyType.Coins,
            currencyAmount = coinsGained
        };
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        coinsGained = 0;
        coinsItem.Source = new CurrencyItem()
        {
            currencyType = CurrencyType.Coins,
            currencyAmount = coinsGained
        };
        scoresText.text = "0";
    }

    private void OnScoresChanged(int prevScores, int newScores)
    {
        scoresText.text = newScores.ToString();
    }

    public void StartSession()
    {
    }

    public void ContinueSession()
    {
        scoresText.text = SessionController.Instance.Scores.ToString();
        coinsGained = prevCoinsGained;
        coinsItem.Source = new CurrencyItem()
        {
            currencyType = CurrencyType.Coins,
            currencyAmount = coinsGained
        };
    }

    public void LoseSession()
    {
        prevCoinsGained = coinsGained;
        CloseWindow();
    }

    public void WinSession()
    {
        CloseWindow();
    }
}
