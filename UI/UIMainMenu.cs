using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Session;
using TMPro;
using Currency;
public class UIMainMenu : UIBaseWindow, ISessionHandler
{
    [SerializeField]
    private List<UICurrencyItem> uiCurrencyItems = new List<UICurrencyItem>();

    public override void Init()
    {
        base.Init();
        EventManager.AddHandler(this);

        var currencyController = GameController.Instance.CurrencyController;
        for(int i = 0; i < currencyController.Currencies.Count; i++)
        {
            var currency = currencyController.Currencies[i];
            uiCurrencyItems[i].Source = currency;
        }
        currencyController.OnCurrencyChanged += OnCurrencyChanged;
    }

    public void OnCurrencyChanged(CurrencyType currencyType, int prevAmount, int newAmount)
    {
        var uiCurrencyItem = uiCurrencyItems.Find(x => x.Source.currencyType == currencyType);
        if (!uiCurrencyItem)
            return;

        uiCurrencyItem.Source = new CurrencyItem()
        {
            currencyType = currencyType,
            currencyAmount = newAmount
        };
    }

    private void OnDestroy()
    {
        EventManager.RemoveHandler(this);
    }

    public void OnPlayButtonClick()
    {
        SessionController.Instance.StartSession();
        CloseWindow();
    }

    private void ProcessWindow<T>(bool selected, string windowId) where T: UIBaseWindow
    {
        var missionWindow = UIMainController.Instance.GetWindow<T>(windowId);
        if (!missionWindow)
            return;

        if (selected)
        {
            missionWindow.OpenWindow();
        }
        else
        {
            missionWindow.CloseWindow();
        }
    }

    public void OnMissionsButtonClick(bool selected)
    {
        ProcessWindow<UIMissionsWindow>(selected, UIConstants.WINDOW_MISSIONS);
    }

    public void OnBankButtonClick(bool selected)
    {
        ProcessWindow<UIBankWindow>(selected, UIConstants.WINDOW_BANK);
    }

    public void OnSkinsButtonClick(bool selected)
    {
        ProcessWindow<UIShopWindow>(selected, UIConstants.WINDOW_SHOP);
    }

    public void StartSession()
    {
        CloseWindow();
    }

    public void WinSession()
    {
    }

    public void LoseSession()
    {
    }
}
