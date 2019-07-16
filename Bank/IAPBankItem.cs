using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IAP;
namespace Bank
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Bank/IAPBankItem")]
    public class IAPBankItem : BankItem, IIAPHandler
    {
        [SerializeField]
        private string iapProductKey;

        [NonSerialized]
        private IAPProduct iapProduct;
        public IAPProduct IAPProduct
        {
            get
            {
                if(iapProduct == null)
                {
                    iapProduct = IAPController.Instance.GetProduct(iapProductKey);
                }
                return iapProduct;
            }
        }

        public override void Buy()
        {
            if(IAPProduct != null)
            {
                EventManager.AddHandler(this);
                IAPController.Instance.BuyProduct(IAPProduct);
            } 
        }

        public void ResultTransaction(IAPProduct product, TransactionResult transactionResult)
        {
            if (IAPProduct != product)
                return;

            if(transactionResult == TransactionResult.AUTHORIZED)
            {
                GameController.Instance.CurrencyController.AddCurrency(reward.currencyType, reward.currencyAmount);
            }
            EventManager.RemoveHandler(this);
        }

        public void SendTransaction(IAPProduct product)
        {
         
        }
    }
}