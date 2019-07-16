using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Rewards;

public class UIFortuneWheelWindow : UIBaseWindow
{
    [SerializeField]
    private Transform spinButton;
    [Header("Reward")]
    [SerializeField]
    private int count;
    [SerializeField]
    private int radius;
    [SerializeField]
    private int startOffsetDeg;
    [SerializeField]
    private int rotateElementOffsetDeg;
    [Header("Wheel")]
    [SerializeField]
    private Transform rotateObject;
    [SerializeField]
    private int cycles;
    [SerializeField]
    private float duration;
    [Header("Pool")]
    [SerializeField]
    private RewardPool pool;

    private RewardPack currentRewardPack;
    private int packIndex;
    private float delta;

    public override void Init()
    {
        base.Init();

        var rewardsResources = ResourceManager.GetResource<RewardResources>(GameConstants.PATH_REWARDS_RESOURCES);
        for(int i = 0; i< rewardsResources.Resources.Count; i++)
        {
            var resource = rewardsResources.Resources[i];
            pool.Templates.Add(resource);
        }

        delta = 2 * Mathf.PI / count;
    }

    public void OpenWindow(NestedWeightedRewardPack nestedRewardPack)
    {
        currentRewardPack = nestedRewardPack.GetPackByWeight(out packIndex);
        currentRewardPack.Use();

        if (nestedRewardPack.Packs.Count != count)
            return;

        spinButton.gameObject.SetActive(true);

        var t = startOffsetDeg * Mathf.Deg2Rad;
        for (int i = 0; i < count; i++)
        {
            var pack = nestedRewardPack.Packs[i];
            for (int j = 0; j < pack.Rewards.Count; j++)
            {
                var rewardSource = pack.Rewards[j];
                var cloned = pool.GetOrInstantiate(rewardSource.Source.TypeId);
                cloned.Source = rewardSource;
                cloned.Init();
                cloned.transform.localPosition = new Vector3(radius * Mathf.Cos(t), radius * Mathf.Sin(t));
                cloned.transform.localEulerAngles = new Vector3(0, 0, t * Mathf.Rad2Deg + rotateElementOffsetDeg);
            }
            t += delta;
        }
        OpenWindow();
    }

    public override void CloseWindow()
    {
        base.CloseWindow();

        pool.ReleaseAll();
    }

    public void OnSpinButtonClick()
    {
        spinButton.gameObject.SetActive(false);
        var angle = (packIndex * delta + delta / 2) * Mathf.Rad2Deg;
        var tweener = rotateObject.DORotate(new Vector3(0f, 0f, cycles * 360 - angle), duration, RotateMode.FastBeyond360);
        tweener.SetEase(Ease.OutQuad);
        tweener.OnComplete(OpenPreviewRewardWindow);
        tweener.Play();
    }

    private void OpenPreviewRewardWindow()
    {
        var window = UIMainController.Instance.GetWindow<UIRewardPreviewWindow>(UIConstants.WINDOW_REWARD_PREVIEW);
        window?.OpenWindow(currentRewardPack);
    }
}