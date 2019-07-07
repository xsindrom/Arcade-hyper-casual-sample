using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Entities;
using Unity.Transforms;

public class PlayerMovementSystem : JobComponentSystem
{

    struct PlayerMovementJobHandler : IJobProcessComponentData<PlayerMovementData, Position>
    {
        public float mousePath;
        
        public void Execute(ref PlayerMovementData movementData, ref Position positionData)
        {
            if (mousePath == 0)
                return;

            positionData.Value.x = movementData.xMultipler * movementData.radius * math.cos(mousePath);
            positionData.Value.y = movementData.yMultipler * movementData.radius * math.sin(mousePath);
        }
    }

    private float3 prevMousePosition;
    private float3 newMousePosition;
    private float mousePath = -Mathf.PI / 2;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var playerCamera = PlayerCameraComponent.PlayerCamera;
            prevMousePosition = playerCamera.ScreenPointToRay(Input.mousePosition).GetPoint(playerCamera.transform.position.z);
        }

        if (Input.GetMouseButton(0))
        {
            var playerCamera = PlayerCameraComponent.PlayerCamera;
            newMousePosition = playerCamera.ScreenPointToRay(Input.mousePosition).GetPoint(playerCamera.transform.position.z);
            mousePath -= newMousePosition.x - prevMousePosition.x;
            prevMousePosition = newMousePosition;
        }

        var playerMovementJobHandler = new PlayerMovementJobHandler()
        {
            mousePath = mousePath
        };
        return playerMovementJobHandler.Schedule(this, inputDeps);
    }
}
