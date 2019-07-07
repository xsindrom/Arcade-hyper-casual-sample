using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Session.Options;

public class MovementObjectsController : MonoBehaviour
{
    protected float minSpeed = 1;
    protected float maxSpeed = 2;
    protected float speedMultipler = 1.0f;

    [SerializeField]
    protected MovementObjectsPool movementObjectsPool;
    protected List<SessionMovementObjectsSpawnHandler> spawnHandlers = new List<SessionMovementObjectsSpawnHandler>();
    public event Action<MovementObject> OnMovementObjectPassed;

    public bool IsStopped { get; set; } = true;

    public void SpawnMovementObject(SessionMovementObjectsSpawnHandler spawnHelper)
    {
        if (spawnHelper.SpawnMaxCount == -1 || spawnHelper.SpawnedCount < spawnHelper.SpawnMaxCount)
        {
            var movementObjectId = spawnHelper.GenerateId();
            var movementObject = movementObjectsPool.GetOrInstantiate(movementObjectId);
            movementObject.SpeedProxy.Value = new SpeedComponentData() { value = minSpeed };
            spawnHelper.SpawnedObjects.Add(movementObject);
            spawnHelper.SpawnedCount++;
        }
    }

    public void Run(SessionMovementObjectsOption option)
    {
        minSpeed = -option.MinSpeed;
        maxSpeed = -option.MaxSpeed;
        speedMultipler = option.SpeedMultiplier;
        spawnHandlers.Clear();

        for(int i = 0; i < option.SpawnHandlers.Count; i++)
        {
            var spawnHandler = option.SpawnHandlers[i];
            if (spawnHandler.Filter.Count == 0)
                continue;

            var timerData = new TimerData()
            {
                id = spawnHandler.name,
                currentTime = 0,
                waitTime = spawnHandler.SpawnInterval,
                fullTime = 0,
                action = () => SpawnMovementObject(spawnHandler)
            };
            spawnHandler.SpawnTimerData = timerData;
            spawnHandlers.Add(spawnHandler);
            Timer.Instance.AddTimer(spawnHandler.SpawnTimerData);
        }
        IsStopped = false;
    }

    public void Stop()
    {
        IsStopped = true;
        for(int i = 0; i < spawnHandlers.Count; i++)
        {
            var spawnHandler = spawnHandlers[i];
            Timer.Instance.RemoveTimer(x=>x.id == spawnHandler.SpawnTimerData.id);
        }

        movementObjectsPool.ReleaseAll();
        
        for(int i = 0; i < spawnHandlers.Count; i++)
        {
            var spawnHandler = spawnHandlers[i];
            spawnHandler.SpawnedObjects.Clear();
        }
    }

    public void ReleaseObject(MovementObject movementObject)
    {
        movementObjectsPool.ReleaseObject(movementObject);
    }

    private void Update()
    {
        if (IsStopped)
            return;

        for(int i = 0; i < spawnHandlers.Count; i++)
        {
            var spawnHelper = spawnHandlers[i];
            for(int j = spawnHelper.SpawnedObjects.Count -1; j>= 0; j--)
            {
                var movementObject = spawnHelper.SpawnedObjects[j];
                if (movementObject.CanPass())
                {
                    movementObject.Pass();
                    OnMovementObjectPassed?.Invoke(movementObject);
                }
                if (movementObject.CanBeReleased())
                {
                    movementObjectsPool.ReleaseObject(movementObject);
                    spawnHelper.SpawnedObjects.Remove(movementObject);
                }
            }
        }
    }
}
