using Bank;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBankSection : MonoBehaviour
{
    [SerializeField]
    private BankItemsStorage storage;
    [SerializeField]
    private UIBankItem bankItemTemplate;
    [SerializeField]
    private RectTransform bankItemsRoot;

    private List<UIBankItem> bankItems = new List<UIBankItem>();

    public void Init()
    {
        for (int i = 0; i < storage.BankItems.Count; i++)
        {
            var source = storage.BankItems[i];
            var clone = Instantiate(bankItemTemplate, bankItemsRoot);
            clone.Source = source;
            clone.Init();
            bankItems.Add(clone);
        }
    }
}
