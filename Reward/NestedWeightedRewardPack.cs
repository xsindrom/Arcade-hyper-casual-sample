using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Reward/NestedWeightedRewardPack")]
    public class NestedWeightedRewardPack : NestedBaseRewardPack<RewardPackWithWeight>
    {
        private static List<int> packsIndexes = new List<int>();

        public RewardPackWithWeight GetPackByWeight(out int packIndex)
        {
            packsIndexes.Clear();
            for(int i = 0; i < packs.Count; i++)
            {
                var pack = packs[i];
                for(int j = 0; j < pack.Weight; j++)
                {
                    packsIndexes.Add(i);
                }
            }

            var randomPackIndex = Random.Range(0, packsIndexes.Count);
            packIndex = packsIndexes[randomPackIndex];
            return packs[packIndex];
        }
    }
}