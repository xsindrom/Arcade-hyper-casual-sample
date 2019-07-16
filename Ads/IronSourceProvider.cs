using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ads
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Ads/Providers/IronSourceProvider")]
    public class IronSourceProvider : AdsProvider
    {
        [SerializeField]
        private string appKey;
        private bool isReady;

        public override bool IsReady()
        {
            return isReady;
        }

        public override void Init()
        {
            base.Init();
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += OnAdsFailed;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += OnAdsCompleted;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += OnIsReadyChanged;

            IronSource.Agent.init(appKey);
            IronSource.Agent.validateIntegration();
            isReady = IronSource.Agent.isRewardedVideoAvailable();
        }

        private void OnIsReadyChanged(bool isReady)
        {
            this.isReady = isReady;
        }

        private void OnAdsCompleted(IronSourcePlacement placement)
        {
            OnAdsCompleted();
        }

        private void OnAdsFailed(IronSourceError error)
        {
            OnAdsFailed();
        }

        public override void Show(AdsPlacement placement)
        {
            base.Show(placement);
            IronSource.Agent.showRewardedVideo(placement.Id);
        }
    }
}