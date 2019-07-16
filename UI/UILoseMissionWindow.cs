using Currency;
using Session;
using Session.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoseMissionWindow : UIBaseWindow
{
    [SerializeField]
    private UICurrencyItem coins;

    private int coinsGained;
    private SessionItemsStorage.SessionData source;
    public SessionItemsStorage.SessionData Source
    {
        get { return source; }
        set { source = value; }
    }

    public override void Init()
    {
        base.Init();
        GameController.Instance.CurrencyController.OnCurrencyChanged += OnCurrencyChanged;
    }

    private void OnCurrencyChanged(CurrencyType type, int prevCount, int newCount)
    {
        if (newCount > prevCount && type == CurrencyType.Coins)
        {
            coinsGained++;
        }
    }

    public override void OpenWindow()
    {
        coins.Source = new CurrencyItem()
        {
            currencyType = CurrencyType.Coins,
            currencyAmount = coinsGained
        };
        coinsGained = 0;
        base.OpenWindow();
    }
    
    public void OnRetryButtonClick()
    {
        if (!Source.IsValid())
            return;

        SessionController.Instance.StartSession(Source);
    }

    public void OnOpenMissionsWindowButtonClick()
    {
        CloseWindow();
        var mainMenu = UIMainController.Instance.GetWindow<UIMissionsWindow>(UIConstants.WINDOW_MISSIONS);
        mainMenu?.OpenWindow();
    }

    public void OnMainMenuButtonClick()
    {
        CloseWindow();
        var mainMenu = UIMainController.Instance.GetWindow<UIMainMenu>(UIConstants.WINDOW_MAIN_MENU);
        mainMenu?.OpenWindow();
    }
}
