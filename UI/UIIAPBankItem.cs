using Bank;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIIAPBankItem : UIBankItem
{
    public override void Init()
    {
        base.Init();
        var iapSource = Source as IAPBankItem;
        if (iapSource)
        {
            price.text = $"{iapSource.IAPProduct.Price} {iapSource.IAPProduct.Currency}";
        }
    }
}
