using Bank;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICurrencyBankItem : UIBankItem
{
    public override void Init()
    {
        base.Init();
        var currencySource = Source as CurrencyBankItem;
        if (currencySource)
        {
            price.text = currencySource.Price.currencyAmount.ToString();
        }
    }
}
