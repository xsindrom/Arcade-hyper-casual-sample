using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ads
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Ads/Providers/UnityAdsProvider")]
    public class UnityAdsProvider : AdsProvider
    {
        public override void Show(AdsPlacement placement)
        {
            base.Show(placement);
            OnAdsCompleted();
        }
    }
}