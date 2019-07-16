using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bank;
using TMPro;

public class UIBankItem : MonoBehaviour, IUIItem<BankItem>
{
    public string Id { get; set; }

    public BankItem Source { get; set; }

    [SerializeField]
    protected TMP_Text reward;
    [SerializeField]
    protected TMP_Text price;

    public virtual void Init()
    {
        Id = Source.Id;
        reward.text = Source.Reward.currencyAmount.ToString();
    }

    public void OnBuyButtonClick()
    {
        Source.Buy();
    }
}
