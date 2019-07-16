using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    public class NestedBaseRewardPack<T> : ScriptableObject where T : RewardPack
    {
        [SerializeField]
        protected string id;
        [SerializeField]
        protected List<T> packs = new List<T>();

        public string Id
        {
            get { return id; }
        }

        public List<T> Packs
        {
            get { return packs; }
        }

        public virtual void Use()
        {
            for(int i = 0; i < packs.Count; i++)
            {
                var pack = packs[i];
                pack.Use();
            }
        }
    }
}
