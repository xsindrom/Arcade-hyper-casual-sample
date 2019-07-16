using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewards;
using TMPro;

public class UIBaseReward : MonoBehaviour, IUIItem<Reward>, IPoolObject
{
    [SerializeField]
    private string typeId;

    public string Id { get { return typeId; } set { typeId = value; } }
    public Reward Source { get; set; }

    public virtual void Init()
    {

    }

    public virtual void ResetObject()
    {

    }
}
