using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

public class MovementObjectSystem : JobComponentSystem
{
    struct MovementObjectJobHandler : IJobProcessComponentData<MovementObjectComponentData, SpeedComponentData, Position>
    {
        public float dt;

        public void Execute(ref MovementObjectComponentData movement, ref SpeedComponentData speed, ref Position position)
        {
            position.Value.z += speed.value * dt;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var movementObjectJobHandler = new MovementObjectJobHandler()
        {
            dt = Time.deltaTime,
        };
        return movementObjectJobHandler.Schedule(this, inputDeps);
    }
}
