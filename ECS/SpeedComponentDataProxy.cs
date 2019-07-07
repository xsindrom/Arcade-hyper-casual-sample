using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct SpeedComponentData : IComponentData
{
    public float value;
}

public class SpeedComponentDataProxy : ComponentDataProxy<SpeedComponentData>
{

}
