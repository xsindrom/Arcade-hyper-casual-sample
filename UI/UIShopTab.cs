using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shop;
public class UIShopTab : UITab
{
    [SerializeField]
    private UIShopItem template;
    [SerializeField]
    private RectTransform shopItemsRoot;
    private List<UIShopItem> shopItems = new List<UIShopItem>();

    private ShopItemsStorage shopItemsStorage;
    public ShopItemsStorage ShopItemsStorage
    {
        get
        {
            if (!shopItemsStorage)
            {
                shopItemsStorage = ResourceManager.GetResource<ShopItemsStorage>(GameConstants.PATH_SHOP_ITEMS_STORAGE);
            }
            return shopItemsStorage;
        }
    }

    private ShopItemsStorage.ShopData shopData;

    public override void InitTab()
    {
        base.InitTab();
        shopData = ShopItemsStorage.ShopItemsData.Find(x => x.Id == Id);
        for(int i = 0; i < shopData.ShopItems.Count; i++)
        {
            var shopItem = shopData.ShopItems[i];
            var clone = Instantiate(template, shopItemsRoot);
            clone.Source = shopItem;
            shopItems.Add(clone);
        }
    }

    public override void OpenTab()
    {
        base.OpenTab();
        for(int i = 0; i < shopItems.Count; i++)
        {
            var shopItem = shopItems[i];
            shopItem.UpdateItem();
        }
        gameObject.SetActive(true);
    }

    public override void CloseTab()
    {
        base.CloseTab();
        gameObject.SetActive(false);
    }

}
