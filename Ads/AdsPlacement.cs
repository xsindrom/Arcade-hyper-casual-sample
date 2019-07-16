using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ads
{
    public abstract class AdsPlacement : ScriptableObject
    {
        [SerializeField]
        protected string id;
        [SerializeField]
        protected int cooldown;

        public string Id
        {
            get { return id; }
        }

        public int Cooldown
        {
            get { return cooldown; }
        }

        public virtual void OnAdsCompleted()
        {

        }

        public virtual void OnAdsFailed()
        {

        }
    }
}