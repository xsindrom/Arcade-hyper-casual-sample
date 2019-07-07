using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct MovementObjectComponentData : IComponentData
{
}

public class MovementObjectComponentDataProxy : ComponentDataProxy<MovementObjectComponentData>
{
}
