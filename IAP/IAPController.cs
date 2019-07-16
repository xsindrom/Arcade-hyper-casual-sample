using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IAP
{
    public class IAPController : MonoSingleton<IAPController>
    {
        public IAPProductsStorage Storage { get; set; }
        public IAPStoreListener Store { get; set; }

        private Dictionary<string, IAPProduct> iapProducts = new Dictionary<string, IAPProduct>();
        public Dictionary<string, IAPProduct> IAPProducts
        {
            get { return iapProducts; }
        }

        public IAPProduct GetProduct(string id)
        {
            return iapProducts.ContainsKey(id) ? iapProducts[id] : null;
        }

        public void AddProduct(IAPProduct product)
        {
            if (product == null)
                return;

            if (!iapProducts.ContainsKey(product.ProductKey))
            {
                iapProducts.Add(product.ProductKey, product);
            }
        }

        public void RemoveProduct(IAPProduct product)
        {
            if (product == null)
                return;

            iapProducts.Remove(product.ProductKey);
        }

        private void ReadFromStorage(IAPProductsStorage storage)
        {
            for(int i = 0; i < storage.IAPProducts.Count; i++)
            {
                var iapProductData = storage.IAPProducts[i];
                var iapProduct = IAPProduct.CreateIapProductByData(iapProductData);
                AddProduct(iapProduct);
            }
        }

        public override void Init()
        {
            base.Init();
            Storage = ResourceManager.GetResource<IAPProductsStorage>(GameConstants.PATH_IAP_PRODUCTS_STORAGE);
#if UNITY_ANDROID
            Store = new GPIAPStoreListener();
#elif UNITY_IOS
            Store = new IOSStoreListener();
#else
            Store = new IAPStoreListener();
#endif
            ReadFromStorage(Storage);
            Store.Initialize();
        }

        public void BuyProduct(IAPProduct product)
        {
            Store.BuyProduct(product);
            EventManager.Call<IIAPHandler>(x => x.SendTransaction(product));
        }

        public void OnPurchaseAuthorized(IAPProduct product)
        {
            EventManager.Call<IIAPHandler>(x => x.ResultTransaction(product, TransactionResult.AUTHORIZED));
        }

        public void OnPurchasePending(IAPProduct product)
        {
            EventManager.Call<IIAPHandler>(x => x.ResultTransaction(product, TransactionResult.PENDING));
        }

        public void OnPurchaseCanceled(IAPProduct product)
        {
            EventManager.Call<IIAPHandler>(x => x.ResultTransaction(product, TransactionResult.CANCEL));
        }

        public void OnPurchaseError(IAPProduct product)
        {
            EventManager.Call<IIAPHandler>(x => x.ResultTransaction(product, TransactionResult.ERROR));
        }
    }
}