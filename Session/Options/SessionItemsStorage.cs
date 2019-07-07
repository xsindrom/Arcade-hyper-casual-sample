using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currency;
namespace Session.Options
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Session/Options/SessionItemsStorage", order = 0)]
    public class SessionItemsStorage : ScriptableObject
    {
        [Serializable]
        public class SessionData
        {
            [SerializeField]
            private string id;
            [SerializeField]
            private CurrencyItem price;
            [SerializeField]
            private SessionItem sessionItem;
            [SerializeField]
            private List<ConditionNode> conditions = new List<ConditionNode>();

            public string Id
            {
                get { return id; }
            }

            public CurrencyItem Price
            {
                get { return price; }
            }

            public SessionItem SessionItem
            {
                get { return sessionItem; }
            }

            public bool IsValid()
            {
                return conditions.Count == 0 ? true : conditions.TrueForAll(x => x.IsValid());
            }
        }
        [SerializeField]
        private List<SessionData> sessions = new List<SessionData>();
        public List<SessionData> Sessions
        {
            get { return sessions; }
        }
    }
}