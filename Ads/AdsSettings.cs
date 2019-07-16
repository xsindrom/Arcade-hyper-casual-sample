using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ads
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Ads/Settings", order = 0)]
    public class AdsSettings : ScriptableObject
    {
        [SerializeField]
        private List<AdsProvider> providers = new List<AdsProvider>();
        [SerializeField]
        private List<AdsPlacement> placements = new List<AdsPlacement>();

        public List<AdsProvider> Providers
        {
            get { return providers; }
        }

        public List<AdsPlacement> Placements
        {
            get { return placements; }
        }
    }
}