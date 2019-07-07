using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Currency
{
    public class CurrencyController : IDisposable
    {
        private CurrencySettings currencySettings;
        public CurrencySettings CurrencySettings
        {
            get
            {
                if (!currencySettings)
                {
                    currencySettings = ResourceManager.GetResource<CurrencySettings>(GameConstants.PATH_CURRENCY_SETTINGS);
                }
                return currencySettings;
            }
        }

        private List<CurrencyItem> currencies = new List<CurrencyItem>();
        public List<CurrencyItem> Currencies
        {
            get { return currencies; }
        }

        public event Action<CurrencyType, int, int> OnCurrencyChanged; 

        public CurrencyController()
        {
            for(int i =0;i < CurrencySettings.Currencies.Count; i++)
            {
                var currency = CurrencySettings.Currencies[i];
                Currencies.Add(currency);
            }

            GameController.Instance.StorageController.OnSave += OnSave;
            GameController.Instance.StorageController.OnLoad += OnLoad;
        }

        public void AddCurrency(CurrencyType currencyType, int amount)
        {
            var currencyIndex = currencies.FindIndex(x => x.currencyType == currencyType);
            if (currencyIndex == -1)
                return;

            var currency = currencies[currencyIndex];
            var prevCurrencyAmount = currency.currencyAmount;
            currency.currencyAmount = currency.currencyAmount + amount < 0 ? 0 : currency.currencyAmount + amount;
            currencies[currencyIndex] = currency;
            OnCurrencyChanged?.Invoke(currencyType, prevCurrencyAmount, currency.currencyAmount);
        }

        public bool TrySubstract(CurrencyType currencyType, int amount)
        {
            if(GetCurrency(currencyType) >= amount)
            {
                AddCurrency(currencyType, -amount);
                return true;
            }
            return false;
        }

        public int GetCurrency(CurrencyType currencyType)
        {
            var currencyIndex = currencies.FindIndex(x => x.currencyType == currencyType);
            return currencyIndex == -1 ? -1 : currencies[currencyIndex].currencyAmount;
        }

        public void OnSave(Storage.StorageData data)
        {
            data.UserData.Currencies.Clear();
            data.UserData.Currencies.AddRange(Currencies);
        }

        public void OnLoad(Storage.StorageData data)
        {
            for(int i = 0; i < data.UserData.Currencies.Count; i++)
            {
                var savedCurrency = data.UserData.Currencies[i];
                var currencyIndex = Currencies.FindIndex(x => x.currencyType == savedCurrency.currencyType);
                if(currencyIndex == -1)
                {
                    Currencies.Add(savedCurrency);
                }
                else
                {
                    Currencies[currencyIndex] = savedCurrency;
                }
            }
        }

        public void Dispose()
        {
            Resources.UnloadAsset(currencySettings);
            currencySettings = null;
            OnCurrencyChanged = null;
        }

    }
}