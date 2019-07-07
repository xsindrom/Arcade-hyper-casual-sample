using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Currency
{
    public enum CurrencyType
    {
        Coins,
        Crystals
    }
    [Serializable]
    public struct CurrencyItem
    {
        public CurrencyType currencyType;
        public int currencyAmount;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(CurrencyItem cur1, CurrencyItem cur2)
        {
            return cur1.currencyType == cur2.currencyType &&
                   cur1.currencyAmount == cur2.currencyAmount;
        }

        public static bool operator !=(CurrencyItem cur1, CurrencyItem cur2)
        {
            return cur1.currencyType != cur2.currencyType ||
                   cur1.currencyAmount != cur2.currencyAmount;
        }
    }

    [CreateAssetMenu(menuName ="ScriptableObjects/Currency/Settings")]
    public class CurrencySettings : ScriptableObject
    {
        [SerializeField]
        private List<CurrencyItem> currencies = new List<CurrencyItem>();
        public List<CurrencyItem> Currencies
        {
            get { return currencies; }
        }
    }
}