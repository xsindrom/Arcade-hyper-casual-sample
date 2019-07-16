using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shop;
using System;

public class UIShopTab : UITab
{
    [SerializeField]
    private UIShopItem template;
    [SerializeField]
    private RectTransform shopItemsRoot;
    private List<UIShopItem> shopItems = new List<UIShopItem>();

    private ShopItemsStorage.ShopData shopData;

    public override void InitTab()
    {
        base.InitTab();
        shopData = GameController.Instance.ShopController.GetShopDataById(Id);
        for(int i = 0; i < shopData.ShopItems.Count; i++)
        {
            var shopItem = shopData.ShopItems[i];
            var clone = Instantiate(template, shopItemsRoot);
            clone.Source = shopItem;
            clone.Owner = this;
            clone.InitItem();
            shopItems.Add(clone);
        }
    }

    public override void OpenTab()
    {
        base.OpenTab();
        UpdateTab();
        gameObject.SetActive(true);
    }

    public override void UpdateTab()
    {
        base.UpdateTab();
        for (int i = 0; i < shopItems.Count; i++)
        {
            var shopItem = shopItems[i];
            shopItem.UpdateItem();
        }
    }

    public override void CloseTab()
    {
        base.CloseTab();
        gameObject.SetActive(false);
    }

    public void UpdateActiveItems(UIShopItem shopItem)
    {
        if (Id != shopItem.Source.GroupId)
            return;

        var shopController = GameController.Instance.ShopController;
        if (shopController.ActiveItems.TryGetValue(Id, out string prevActiveSkinId))
        {
            if (prevActiveSkinId != shopItem.Source.Id)
            {
                shopController.ActiveItems[Id] = shopItem.Source.Id;
                var prevActiveSkin = shopItems.Find(x => x.Source.Id == prevActiveSkinId);
                if (prevActiveSkin)
                {
                    prevActiveSkin.Source.State = State.Bought;
                    prevActiveSkin.UpdateItem();
                }
            }
        }
        else
        {
            shopController.ActiveItems.Add(Id, shopItem.Source.Id);
        }
        shopItem.UpdateItem();
    }
}
