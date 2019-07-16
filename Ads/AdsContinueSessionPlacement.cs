using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Session;

namespace Ads
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Ads/Placements/AdsContinueSessionPlacement")]
    public class AdsContinueSessionPlacement : AdsPlacement
    {
        public override void OnAdsCompleted()
        {
            base.OnAdsCompleted();
            SessionController.Instance.ContinueSession();
        }
    }
}