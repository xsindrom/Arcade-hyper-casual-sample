using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    [Serializable]
    public class Reward
    {
        [SerializeField]
        private RewardSource source;
        [SerializeField]
        private int count;
        
        public RewardSource Source
        {
            get { return source; }
        }

        public int Count
        {
            get { return count; }
        }

        public void Use()
        {
            source.Use(count);
        }
    }
}