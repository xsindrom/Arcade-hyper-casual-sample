using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Storage;

namespace Shop
{
    public class ShopController : IStorageHandler, IDisposable
    {
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

        private Dictionary<string, string> activeItems = new Dictionary<string, string>();
        public Dictionary<string, string> ActiveItems
        {
            get { return activeItems; }
        }

        private List<string> boughtItems = new List<string>();
        public List<string> BoughtItems
        {
            get { return boughtItems; }
        }


        public ShopController()
        {
            for (int i = 0; i < ShopItemsStorage.ShopItemsData.Count; i++)
            {
                var data = ShopItemsStorage.ShopItemsData[i];
                for (int j = 0; j < data.ShopItems.Count; j++)
                {
                    var item = data.ShopItems[j];
                    switch (item.State)
                    {
                        case State.Bought:
                            boughtItems.Add(item.Id);
                            break;
                        case State.Active:
                            boughtItems.Add(item.Id);
                            activeItems.Add(item.GroupId, item.Id);
                            break;
                        default: continue;
                    }
                }
            }

            EventManager.AddHandler(this);
        }

        public ShopItemsStorage.ShopData GetShopDataById(string id)
        {
            return ShopItemsStorage.ShopItemsData.Find(x => x.Id == id);
        }

        public void OnSave(StorageData data)
        {
            data.ShopData.ActiveItems.Clear();
            foreach (var item in activeItems)
            {
                data.ShopData.ActiveItems.Add($"{item.Key}.{item.Value}");
            }
            data.ShopData.BoughtItems.Clear();
            data.ShopData.BoughtItems.AddRange(BoughtItems);
        }

        public void OnLoad(StorageData data)
        {
            for (int i = 0; i < data.ShopData.ActiveItems.Count; i++)
            {
                var item = data.ShopData.ActiveItems[i];
                var splitItems = item.Split('.');
                if (splitItems.Length == 2)
                {
                    var groupId = splitItems[0];
                    var itemId = splitItems[1];
                    if (!string.IsNullOrEmpty(groupId) && !string.IsNullOrEmpty(itemId))
                    {
                        activeItems[groupId] = itemId;
                    }
                }
            }

            for (int i = 0; i < data.ShopData.BoughtItems.Count; i++)
            {
                var boughtItem = data.ShopData.BoughtItems[i];
                if (!boughtItems.Contains(boughtItem))
                {
                    boughtItems.Add(boughtItem);
                }
            }

            for (int i = 0; i < ShopItemsStorage.ShopItemsData.Count; i++)
            {
                var shopItemsData = ShopItemsStorage.ShopItemsData[i];
                for (int j = 0; j < shopItemsData.ShopItems.Count; j++)
                {
                    var item = shopItemsData.ShopItems[j];
                    InitItem(item);
                }
            }
        }

        private void InitItem(ShopItem item)
        {
            if (activeItems.ContainsValue(item.Id))
            {
                item.State = State.Active;
                return;
            }
            if (boughtItems.Contains(item.Id))
            {
                item.State = State.Bought;
                return;
            }
            if (item.IsValid())
            {
                item.State = State.Available;
                return;
            }
            item.State = State.Locked;
        }

        public void Dispose()
        {
            Resources.UnloadAsset(shopItemsStorage);
            shopItemsStorage = null;
            EventManager.RemoveHandler(this);
        }
    }
}
