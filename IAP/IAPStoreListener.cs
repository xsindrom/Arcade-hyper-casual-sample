using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
namespace IAP
{
    public interface IIAPStoreListener
    {
        void BuyProduct(IAPProduct product);
        void Initialize();
    }

    public class IAPStoreListener : IStoreListener, IIAPStoreListener
    {
        protected IStoreController storeController;
        protected IExtensionProvider storeExtensions;

        public bool IsInitialized()
        {
            return storeController != null && storeExtensions != null;
        }


        public void Initialize()
        {
            if (IsInitialized())
                return;

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach(var iapProduct in IAPController.Instance.IAPProducts)
            {
                builder.AddProduct(iapProduct.Key, iapProduct.Value.ProductType, new IDs()
                {
                    { iapProduct.Value.GPProductKey, GooglePlay.Name},
                    {iapProduct.Value.IOSProductKey, AppleAppStore.Name }
                });
            }
            UnityPurchasing.Initialize(this,builder);
        }

       
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            storeExtensions = extensions;

            foreach(var iapProduct in IAPController.Instance.IAPProducts)
            {
                iapProduct.Value.Product = controller.products.WithID(iapProduct.Key);
            }
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("IAPMobileStoreListener on initialized failed error: " + error);
        }

        public void BuyProduct(IAPProduct iapProduct)
        {
            var product = storeController.products.WithID(iapProduct.ProductKey);
            if(product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product);
            }
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            var iapProduct = IAPController.Instance.GetProduct(e.purchasedProduct.definition.id);
            if(iapProduct != null)
            {
                var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
                var valid = false;
                try
                {
                    var result = validator.Validate(e.purchasedProduct.receipt);
                    foreach (IPurchaseReceipt receipt in result)
                    {
#if UNITY_ANDROID || UNITY_STANDALONE
                        var google = receipt as GooglePlayReceipt;
                        if (null != google)
                        {
                            Debug.Log(google.transactionID);
                            Debug.Log(google.purchaseState);
                            Debug.Log(google.purchaseToken);
                        }
#elif UNITY_IOS
                        AppleInAppPurchaseReceipt apple = receipt as AppleInAppPurchaseReceipt;
                        if (null != apple) 
                        {
                            Debug.Log(apple.originalTransactionIdentifier);
                            Debug.Log(apple.subscriptionExpirationDate);
                            Debug.Log(apple.cancellationDate);
                            Debug.Log(apple.quantity);
                        }
#endif
                    }
                    valid = true;
                }
                catch (IAPSecurityException secureException)
                {
                    Debug.Log(secureException.ToString());
                    valid = false;
                }

                if (valid)
                {
                    IAPController.Instance.OnPurchaseAuthorized(iapProduct);
                }
                else
                {
                    IAPController.Instance.OnPurchaseError(iapProduct);
                }
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            var iapProduct = IAPController.Instance.GetProduct(product.definition.id);
            if (reason == PurchaseFailureReason.UserCancelled)
            {
                IAPController.Instance.OnPurchaseCanceled(iapProduct);
            }
            else
            {
                IAPController.Instance.OnPurchaseError(iapProduct);
            }
        }

        public void ConfirmPurchase(IAPProduct product)
        {
            storeController.ConfirmPendingPurchase(product.Product);
        }
    }
}