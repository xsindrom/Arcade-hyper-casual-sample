using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ads
{
    public abstract class AdsProvider : ScriptableObject
    {
        [SerializeField]
        protected string id;
        public string Id
        {
            get { return id; }
        }
        protected AdsPlacement CurrentPlacement { get; set; }

        public virtual bool IsReady()
        {
            return true;
        }

        public virtual void Init()
        {

        }

        public virtual void Show(AdsPlacement placement)
        {
            CurrentPlacement = placement;
        }

        public virtual void OnAdsCompleted()
        {
            CurrentPlacement?.OnAdsCompleted();
            CurrentPlacement = null;
        }

        public virtual void OnAdsFailed()
        {
            CurrentPlacement?.OnAdsFailed();
            CurrentPlacement = null;
        }
    }
}