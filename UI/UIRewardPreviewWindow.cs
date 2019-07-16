using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewards;

public class UIRewardPreviewWindow : UIBaseWindow
{
    [SerializeField]
    private RewardPool pool;

    private RewardPack pack;

    public override void Init()
    {
        base.Init();

        var rewardsResources = ResourceManager.GetResource<RewardResources>(GameConstants.PATH_PREVIEW_REWARDS_RESOURCES);
        for (int i = 0; i < rewardsResources.Resources.Count; i++)
        {
            var resource = rewardsResources.Resources[i];
            pool.Templates.Add(resource);
        }
    }

    public override void OpenWindow()
    {
        for (int j = 0; j < pack.Rewards.Count; j++)
        {
            var rewardSource = pack.Rewards[j];
            var cloned = pool.GetOrInstantiate(rewardSource.Source.TypeId);
            cloned.Source = rewardSource;
            cloned.Init();
        }

        base.OpenWindow();
    }

    public void OpenWindow(RewardPack pack)
    {
        this.pack = pack;
        OpenWindow();
    }

    public override void CloseWindow()
    {
        base.CloseWindow();

        pool.ReleaseAll();
    }
}
