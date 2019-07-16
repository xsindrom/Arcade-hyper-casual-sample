using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currency;

namespace Bank
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Bank/CurrencyBankItem")]
    public class CurrencyBankItem : BankItem
    {
        [SerializeField]
        private CurrencyItem price;
        public CurrencyItem Price
        {
            get { return price; }
        }

        public override void Buy()
        {
            if(GameController.Instance.CurrencyController.TrySubstract(price.currencyType, price.currencyAmount))
            {
                GameController.Instance.CurrencyController.AddCurrency(reward.currencyType, reward.currencyAmount);
            }
        }
    }
}