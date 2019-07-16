using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Currency;
public class UICurrencyItem : MonoBehaviour, IUIItem<CurrencyItem>
{
    public string Id { get; set; }

    private CurrencyItem source;
    public CurrencyItem Source
    {
        get { return source; }
        set
        {
            source = value;
            OnSourceChanged();
        }
    }
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text amountText;
    [SerializeField]
    private string amountTextFormat = "{0}";

    public Image Icon
    {
        get { return icon; }
    }

    public void OnSourceChanged()
    {
        amountText.text = string.Format(amountTextFormat, Source.currencyAmount);
        if (icon.sprite)
            return;

        var currencyIcons = ResourceManager.GetResource<SpriteResources>(GameConstants.PATH_CURRENCY_SPRITE_RESOURCES);
        if (currencyIcons)
        {
            icon.sprite = currencyIcons.Resources.Find(x => x.name == Source.currencyType.ToString());
        }
    }
}
