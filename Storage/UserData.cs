using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currency;
namespace Storage
{
    [Serializable]
    public class UserData
    {
        [SerializeField]
        private int level;
        [SerializeField]
        private int experience;
        [SerializeField]
        private int bonusStartTime;
        [SerializeField]
        private List<CurrencyItem> currencies = new List<CurrencyItem>();

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public int Experince
        {
            get { return experience; }
            set { experience = value; }
        }

        public List<CurrencyItem> Currencies
        {
            get { return currencies; }
        }

        public int BonusStartTime
        {
            get { return bonusStartTime; }
            set { bonusStartTime = value; }
        }
    }
}