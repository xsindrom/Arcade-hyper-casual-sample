using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Session;
using Currency;
namespace Shop
{
    public enum State
    {
        Locked,
        Available,
        Bought,
        Active
    }

    public abstract class ShopItem : ScriptableObject
    {
        [SerializeField]
        protected string id;
        [SerializeField]
        protected CurrencyItem price;
        [SerializeField]
        protected State initialState;
        [SerializeField]
        protected List<ConditionNode> conditions = new List<ConditionNode>();

        [NonSerialized]
        protected State state;
        [NonSerialized]
        protected string groupId;

        public string Id
        {
            get { return id; }
        }

        public CurrencyItem Price
        {
            get { return price; }
        }

        public State State
        {
            get { return state; }
            set { state = value; }
        }

        public string GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        private void OnEnable()
        {
            state = initialState;
        }

        public bool IsValid()
        {
            return conditions.Count == 0 ? true : conditions.TrueForAll(x => x.IsValid());
        }

        public virtual void Buy()
        {
            if (GameController.Instance.CurrencyController.TrySubstract(Price.currencyType, Price.currencyAmount))
            {
                GameController.Instance.ShopController.BoughtItems.Add(Id);
                state = State.Bought;
            }
        }

        public virtual void Activate()
        {
            state = State.Active;
        }
    }
}