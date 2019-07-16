using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Shop/ShopItemsStorage", order = 0)]
    public class ShopItemsStorage : ScriptableObject
    {
        [Serializable]
        public class ShopData
        {
            [SerializeField]
            private string id;
            [SerializeField]
            private List<ShopItem> shopItems = new List<ShopItem>();

            public string Id
            {
                get { return id; }
            }

            public List<ShopItem> ShopItems
            {
                get { return shopItems; }
            }
        }

        [SerializeField]
        private List<ShopData> shopItemsData = new List<ShopData>();
        public List<ShopData> ShopItemsData
        {
            get { return shopItemsData; }
        }

        private void OnEnable()
        {
            for(int i = 0; i < shopItemsData.Count; i++)
            {
                var data = shopItemsData[i];
                for(int j = 0; j<data.ShopItems.Count; j++)
                {
                    var item = data.ShopItems[j];
                    item.GroupId = data.Id;
                }
            }
        }
    }
}