using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewards;
using Currency;

public class UICurrencyReward : UIBaseReward
{
    [SerializeField]
    private UICurrencyItem currencyItem;

    public override void Init()
    {
        base.Init();

        var currencySource = Source.Source as CurrencySource;
        if (!currencySource)
            return;

        currencyItem.Source = new CurrencyItem()
        {
            currencyType = currencySource.CurrencyType,
            currencyAmount = Source.Count
        };
    }

    public override void ResetObject()
    {
        base.ResetObject();

        currencyItem.Icon.sprite = null;
    }
}
