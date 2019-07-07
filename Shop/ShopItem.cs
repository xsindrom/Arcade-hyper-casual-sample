using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Session;
using Currency;
namespace Shop
{
    public abstract class ShopItem : ScriptableObject
    {
        [SerializeField]
        protected string id;
        [SerializeField]
        protected CurrencyItem price;
        [SerializeField]
        protected List<ConditionNode> conditions = new List<ConditionNode>();

        public string Id
        {
            get { return id; }
        }

        public CurrencyItem Price
        {
            get { return price; }
        }

        public virtual bool IsAvailable()
        {
            return conditions.Count == 0 ? true : conditions.TrueForAll(x => x.IsValid());
        }

        public bool IsBought()
        {
            return GameController.Instance.StorageController.StorageData.ShopData.BoughtItems.Contains(Id);
        }

        public virtual void Buy()
        {
            if (GameController.Instance.CurrencyController.TrySubstract(Price.currencyType, Price.currencyAmount))
            {
                GameController.Instance.StorageController.StorageData.ShopData.BoughtItems.Add(Id);
            }
        }

        public virtual void Activate()
        {

        }
    }
}