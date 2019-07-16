using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bank
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Bank/Storage")]
    public class BankItemsStorage : ScriptableObject
    {
        [SerializeField]
        private List<BankItem> bankItems = new List<BankItem>();
        public List<BankItem> BankItems
        {
            get { return bankItems; }
        }
    }
}