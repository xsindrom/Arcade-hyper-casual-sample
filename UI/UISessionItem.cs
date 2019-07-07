using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Session;
using Session.Options;
using TMPro;

public class UISessionItem : MonoBehaviour, IUIItem<SessionItemsStorage.SessionData>
{
    [SerializeField]
    private string id;
    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    private SessionItemsStorage.SessionData source;
    public SessionItemsStorage.SessionData Source
    {
        get { return source; }
        set { source = value; }
    }
    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private UICurrencyItem price;

    [SerializeField]
    private RectTransform completedState;
    [SerializeField]
    private RectTransform boughtState;
    [SerializeField]
    private RectTransform readyToBuyState;

    public void UpdateItem()
    {
        titleText.text = Id;
        price.Source = Source.Price;

        var data = GameController.Instance.StorageController.StorageData.SessionsData;
        if (data.AvailableSessions.Contains(Source.Id))
        {
            SetBoughtState();
        }
        else if (data.CompletedSessions.Contains(Source.Id))
        {
            SetCompletedState();
        }
        else
        {
            SetReadyToBuyState();
        }
    }

    public void SetCompletedState()
    {
        completedState.gameObject.SetActive(true);
        boughtState.gameObject.SetActive(false);
        readyToBuyState.gameObject.SetActive(false);
    }

    public void SetBoughtState()
    {
        completedState.gameObject.SetActive(false);
        boughtState.gameObject.SetActive(true);
        readyToBuyState.gameObject.SetActive(false);
    }

    public void SetReadyToBuyState()
    {
        completedState.gameObject.SetActive(false);
        boughtState.gameObject.SetActive(false);
        readyToBuyState.gameObject.SetActive(true);
    }

    public void OnPlayButtonClick()
    {
        if (!Source.IsValid())
            return;

        SessionController.Instance.StartSession(Source);
    }

    public void OnBuyButtonClick()
    {
        if (GameController.Instance.CurrencyController.TrySubstract(Source.Price.currencyType, Source.Price.currencyAmount))
        {
            GameController.Instance.StorageController.StorageData.SessionsData.AvailableSessions.Add(Source.Id);
            SetBoughtState();
        }
    }
}
