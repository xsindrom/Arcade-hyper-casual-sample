using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Reward/RewardPackWithWeight")]
    public class RewardPackWithWeight : RewardPack
    {
        [SerializeField]
        private int weight;

        public int Weight
        {
            get { return weight; }
        }
    }
}
