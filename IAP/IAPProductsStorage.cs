using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
namespace IAP
{
    [Serializable]
    public struct IAPProductData
    {
        public string productKey;
        public string storeKey;
        public string gpProductKey;
        public string iosProductKey;
        public float price;
        public ProductType productType;
    }

    [CreateAssetMenu(menuName ="ScriptableObjects/IAP/Storage", order = 0)]
    public class IAPProductsStorage : ScriptableObject
    {
        [SerializeField]
        private List<IAPProductData> iapProducts = new List<IAPProductData>();
        public List<IAPProductData> IAPProducts
        {
            get { return iapProducts; }
        }

    }
}