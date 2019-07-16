using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewards;

public class UISkinReward : UIBaseReward
{
    [SerializeField]
    private Image icon;

    public override void Init()
    {
        base.Init();

        var skinSource = Source.Source as SkinSource;
        if (!skinSource)
            return;

        var iconSprites = ResourceManager.GetResource<SpriteResources>(GameConstants.PATH_SHOP_ICONS_SPRITE_RESOURCES);
        var skinIcon = iconSprites.Resources.Find(x => x.name == skinSource.SkinId);
        icon.sprite = skinIcon;
    }

    public override void ResetObject()
    {
        base.ResetObject();

        icon.sprite = null;
    }
}
