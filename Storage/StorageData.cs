using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    [Serializable]
    public class StorageData
    {
        [SerializeField]
        private UserData userData = new UserData();
        [SerializeField]
        private SessionsData sessionsData = new SessionsData();
        [SerializeField]
        private ShopData shopData = new ShopData();
        [SerializeField]
        private AdsData adsData = new AdsData();

        public UserData UserData
        {
            get { return userData; }
        }

        public SessionsData SessionsData
        {
            get { return sessionsData; }
        }

        public ShopData ShopData
        {
            get { return shopData; }
        }

        public AdsData AdsData
        {
            get { return adsData; }
        }
    }
}