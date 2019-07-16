using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Reward/RewardPack")]
    public class RewardPack : ScriptableObject
    {
        [SerializeField]
        protected string id;
        [SerializeField]
        protected List<Reward> rewards = new List<Reward>();

        public string Id
        {
            get { return id; }
        }

        public List<Reward> Rewards
        {
            get { return rewards; }
        }

        public virtual void Use()
        {
            for (int i = 0; i < rewards.Count; i++)
            {
                var reward = rewards[i];
                reward.Use();
            }
        }
    }
}