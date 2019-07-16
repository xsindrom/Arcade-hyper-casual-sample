using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewards;
namespace Ads
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Ads/Placements/AdsFortuneWheelPlacement")]
    public class AdsFortuneWheelPlacement : AdsPlacement
    {
        [SerializeField]
        private NestedWeightedRewardPack rewardPack;

        public NestedWeightedRewardPack RewardPack
        {
            get { return rewardPack; }
        }

        public override void OnAdsCompleted()
        {
            base.OnAdsCompleted();
            var window = UIMainController.Instance.GetWindow<UIFortuneWheelWindow>(UIConstants.WINDOW_FORTUNE);
            window?.OpenWindow(rewardPack);
        }

    }
}