using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currency;

namespace Bank
{
    public abstract class BankItem : ScriptableObject
    {
        [SerializeField]
        protected string id;
        public string Id
        {
            get { return id; }
        }
        [SerializeField]
        protected CurrencyItem reward;
        public CurrencyItem Reward
        {
            get { return reward; }
        }

        public abstract void Buy();
    }
}