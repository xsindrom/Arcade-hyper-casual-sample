using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shop;

public class UIShopWindow : UIBaseWindow
{
    [SerializeField]
    private TabGroup tabs;
    [SerializeField]
    private UIShopTab tabTemplate;

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

    public override void Init()
    {
        base.Init();
        for(int i = 0; i < ShopItemsStorage.ShopItemsData.Count; i++)
        {
            var shopItem = ShopItemsStorage.ShopItemsData[i];
            var cloned = Instantiate(tabTemplate, tabs.TabsRoot);
            cloned.Id = shopItem.Id;
            cloned.InitTab();
            tabs.AddTab(cloned);
        }
        tabs.OpenTab(0);
    }
}
