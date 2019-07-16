using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Reward/CurrencySource")]
    public class SkinSource : RewardSource
    {
        [SerializeField]
        private string skinId;

        public string SkinId
        {
            get { return skinId; }
        }

        public override void Use(int count)
        {

        }
    }
}