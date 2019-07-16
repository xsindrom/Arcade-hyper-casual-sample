using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
namespace IAP
{
    [Serializable]
    public class IAPProduct
    {
        public string ProductKey { get; set; }
        public string StoreKey { get; set; }
        public float Price { get; set; }
        public string Currency { get { return "USD"; } }
        public Product Product { get; set; }
        public ProductType ProductType { get; set; }
        public string GPProductKey { get; set; }
        public string IOSProductKey { get; set; }

        public static IAPProduct CreateIapProductByData(IAPProductData data)
        {
            var iapProduct = new IAPProduct()
            {
                ProductKey = data.productKey,
                StoreKey = data.storeKey,
                Price = data.price,
                ProductType = data.productType,
                GPProductKey = data.gpProductKey,
                IOSProductKey = data.iosProductKey
            };
            iapProduct.Init();
            return iapProduct;
        }

        public virtual void Init() { }
    }
}