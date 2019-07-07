using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct PlayerMovementData : IComponentData
{
    public float radius;
    public float xMultipler;
    public float yMultipler; 
}

public class PlayerMovementDataProxy : ComponentDataProxy<PlayerMovementData>
{

}