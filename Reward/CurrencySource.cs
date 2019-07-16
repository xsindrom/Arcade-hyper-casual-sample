using Currency;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Reward/CurrencySource")]
    public class CurrencySource : RewardSource
    {
        [SerializeField]
        private CurrencyType currencyType;

        public CurrencyType CurrencyType
        {
            get { return currencyType; }
        }

        public override void Use(int count)
        {
            GameController.Instance.CurrencyController.AddCurrency(currencyType, count);
        }
    }
}