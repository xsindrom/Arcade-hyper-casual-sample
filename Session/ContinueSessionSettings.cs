using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currency;

namespace Session
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Session/ContinueSessionSettings")]
    public class ContinueSessionSettings : ScriptableObject
    {
        [SerializeField]
        private string adsPlacement;
        [SerializeField]
        private List<CurrencyItem> prices = new List<CurrencyItem>();

        public string AdsPlacement
        {
            get { return adsPlacement; }
        }

        public List<CurrencyItem> Prices
        {
            get { return prices; }
        }
    }
}