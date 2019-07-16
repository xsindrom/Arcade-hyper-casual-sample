using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shop;
using System;

public class UIShopItem : MonoBehaviour, IUIItem<ShopItem>
{
    [Serializable]
    public struct StateData
    {
        public State state;
        public RectTransform target;

        public void Activate()
        {
            target.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            target.gameObject.SetActive(false);
        }

        public void UpdateState(State state)
        {
            if(this.state == state)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }
    }

    public string Id { get; set; }

    public ShopItem Source { get; set; }
    public UIShopTab Owner { get; set; }

    [SerializeField]
    private Image icon;
    [SerializeField]
    private UICurrencyItem price;
    [SerializeField]
    private List<StateData> states = new List<StateData>();


    public void InitItem()
    {
        var iconSprites = ResourceManager.GetResource<SpriteResources>(GameConstants.PATH_SHOP_ICONS_SPRITE_RESOURCES);
        icon.sprite = iconSprites.Resources.Find(x => x.name == Source.Id);
        price.Source = Source.Price;
        UpdateItem();
    }

    public void UpdateItem()
    {
        for (int i = 0; i < states.Count; i++)
        {
            var stateData = states[i];
            stateData.UpdateState(Source.State);
        }
    }
    
    public void OnBuyButtonClick()
    {
        Source.Buy();
        if (Source.State == State.Bought)
        {
            UpdateItem();
        }       
    }

    public void OnActivateButtonClick()
    {
        Source.Activate();
        Owner.UpdateActiveItems(this);
    }
}
