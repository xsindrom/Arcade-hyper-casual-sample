using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewards;

[CreateAssetMenu(menuName = "ScriptableObjects/DailyBonus/DailyBonusSettings")]
public class DailyBonusSettings : ScriptableObject
{
    [SerializeField]
    private NestedWeightedRewardPack pack;
    [SerializeField]
    private int repeatTimeInSeconds;

    public NestedWeightedRewardPack Pack
    {
        get { return pack; }
    }

    public int RepeatTimeInSeconds
    {
        get { return repeatTimeInSeconds; }
    }
}
