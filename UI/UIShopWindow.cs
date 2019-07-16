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

    public override void Init()
    {
        base.Init();
        var shopItemsData = GameController.Instance.ShopController.ShopItemsStorage.ShopItemsData;
        for (int i = 0; i < shopItemsData.Count; i++)
        {
            var shopItem = shopItemsData[i];
            var cloned = Instantiate(tabTemplate, tabs.TabsRoot);
            cloned.Id = shopItem.Id;
            cloned.InitTab();
            tabs.AddTab(cloned);
        }
        tabs.OpenTab(0);
    }
}
