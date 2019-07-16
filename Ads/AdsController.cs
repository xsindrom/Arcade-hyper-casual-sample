using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Storage;

namespace Ads
{
    public class AdsController: IStorageHandler, IDisposable
    {
        private AdsSettings settings;
        public AdsSettings Settings
        {
            get
            {
                if (!settings)
                {
                    settings = ResourceManager.GetResource<AdsSettings>(GameConstants.PATH_ADS_SETTINGS);
                }
                return settings;
            }
        }

        private List<AdsInfo> adsInfos = new List<AdsInfo>();

        public AdsController()
        {
            EventManager.AddHandler(this);
            for(int i = 0; i < Settings.Providers.Count; i++)
            {
                var provider = Settings.Providers[i];
                provider.Init();
            }
        }

        public bool IsPlacementReady(AdsPlacement placement)
        {
            var adsIndex = adsInfos.FindIndex(x => x.placement == placement.Id);
            return adsIndex == -1 ||
                   adsInfos[adsIndex].startTime + placement.Cooldown < DateTime.Now.ToUnixTime();
        }

        public void Show(string placement)
        {
            var adsPlacement = Settings.Placements.Find(x => x.Id == placement);
            if (!adsPlacement || !IsPlacementReady(adsPlacement))
                return;

            var provider = Settings.Providers.Find(x => x.IsReady());
            if (!provider)
                return;

            provider.Show(adsPlacement);

            var adsIndex = adsInfos.FindIndex(x => x.placement == adsPlacement.Id);
            if (adsIndex == -1)
            {
                adsInfos.Add(new AdsInfo() { placement = adsPlacement.Id, startTime = DateTime.Now.ToUnixTime() });
            }
            else
            {
                adsInfos[adsIndex] = new AdsInfo() { placement = adsPlacement.Id, startTime = DateTime.Now.ToUnixTime() };
            }
        }

        public void OnLoad(StorageData data)
        {
            adsInfos.Clear();
            for(int i = 0; i < data.AdsData.Ads.Count; i++)
            {
                var adsInfo = data.AdsData.Ads[i];
                adsInfos.Add(adsInfo);
            }
        }

        public void OnSave(StorageData data)
        {
            for(int i = 0; i < adsInfos.Count; i++)
            {
                var adsInfo = adsInfos[i];
                var dataIndex = data.AdsData.Ads.FindIndex(x => x.placement == adsInfo.placement);
                if(dataIndex == -1)
                {
                    data.AdsData.Ads.Add(adsInfo);
                }
                else
                {
                    data.AdsData.Ads[dataIndex] = adsInfo;
                }
            }
        }

        public void Dispose()
        {
            Resources.UnloadAsset(settings);
            EventManager.RemoveHandler(this);
        }
    }
}