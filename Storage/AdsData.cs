using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    [Serializable]
    public struct AdsInfo
    {
        public string placement;
        public int startTime;
    }

    [Serializable]
    public class AdsData
    {
        [SerializeField]
        private List<AdsInfo> ads = new List<AdsInfo>();

        public List<AdsInfo> Ads
        {
            get { return ads; }
        }
    }
}