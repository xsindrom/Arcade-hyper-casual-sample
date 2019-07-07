using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shop;

public class UIShopItem : MonoBehaviour, IUIItem<ShopItem>
{
    public string Id { get; set; }

    private ShopItem source;
    public ShopItem Source
    {
        get { return source; }
        set { source = value; }
    }
    [SerializeField]
    private Image icon;
    [SerializeField]
    private UICurrencyItem price;
    [SerializeField]
    private RectTransform boughtState;
    [SerializeField]
    private RectTransform lockedState;
    [SerializeField]
    private RectTransform readyToBuyState;

    public void UpdateItem()
    {
        var iconSprites = ResourceManager.GetResource<SpriteResources>(GameConstants.PATH_SHOP_ICONS_SPRITE_RESOURCES);
        icon.sprite = iconSprites.Resources.Find(x => x.name == Source.Id);
        price.Source = Source.Price;

        if (!Source.IsAvailable())
        {
            SetLockedState();
        }
        else if(Source.IsBought())
        {
            SetBoughtState();
        }
        else
        {
            SetReadyToBuyState();
        }
    }

    public void SetReadyToBuyState()
    {
        readyToBuyState.gameObject.SetActive(true);
        lockedState.gameObject.SetActive(false);
        boughtState.gameObject.SetActive(false);
    }

    public void SetBoughtState()
    {
        readyToBuyState.gameObject.SetActive(false);
        lockedState.gameObject.SetActive(false);
        boughtState.gameObject.SetActive(true);
    }

    public void SetLockedState()
    {
        readyToBuyState.gameObject.SetActive(false);
        lockedState.gameObject.SetActive(true);
        boughtState.gameObject.SetActive(false);
    }

    public void OnBuyButtonClick()
    {
        Source.Buy();
        if (Source.IsBought())
        {
            SetBoughtState();
        }
    }

    public void OnActivateButtonClick()
    {
        Source.Activate();
    }
}
