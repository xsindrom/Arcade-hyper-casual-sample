using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    public abstract class RewardSource : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private string typeId;
        
        public string Id
        {
            get { return id; }
        }
        public string TypeId
        {
            get { return typeId; }
        }

        public abstract void Use(int count);
    }
}