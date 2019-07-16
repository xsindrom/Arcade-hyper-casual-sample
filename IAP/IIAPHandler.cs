using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IAP
{
    public enum TransactionResult
    {
        AUTHORIZED,
        PENDING,
        CANCEL,
        ERROR
    }

    public interface IIAPHandler : IEventHandler
    {
        void SendTransaction(IAPProduct product);
        void ResultTransaction(IAPProduct product, TransactionResult transactionResult);
    }
}