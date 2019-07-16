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
    protected float prevSpeed;
    protected float currentSpeed;
    protected int releaseObjectsSpeedUpCount = 5;
    [SerializeField]
    protected Vector3 startPosition;
    [SerializeField]
    protected Vector3 passPosition;
    [SerializeField]
    protected Vector3 releasePosition;

    [SerializeField]
    protected MovementObjectsPool movementObjectsPool;
    protected List<SessionMovementObjectsSpawnHandler> spawnHandlers = new List<SessionMovementObjectsSpawnHandler>();
    public event Action<MovementObject> OnMovementObjectPassed;

    public bool IsStopped { get; set; } = true;
    public int ObjectsReleased { get; set; }

    public void SpawnMovementObject(SessionMovementObjectsSpawnHandler spawnHelper)
    {
        if (spawnHelper.SpawnMaxCount == -1 || spawnHelper.SpawnedCount < spawnHelper.SpawnMaxCount)
        {
            var movementObjectId = spawnHelper.GenerateId();
            var movementObject = movementObjectsPool.GetOrInstantiate(movementObjectId);
            movementObject.SpeedProxy.Value = new SpeedComponentData() { value = currentSpeed };
            movementObject.PositionProxy.Value = new Unity.Transforms.Position() { Value = startPosition };
            movementObject.transform.position = startPosition;
            movementObject.StartPosition = startPosition;
            movementObject.PassPosition = passPosition;
            movementObject.ReleasePosition = releasePosition;

            spawnHelper.SpawnedObjects.Add(movementObject);
            spawnHelper.SpawnedCount++;
        }
    }

    public void SpeedUp()
    {
        prevSpeed = currentSpeed;
        currentSpeed = Mathf.Clamp(currentSpeed + speedMultipler * currentSpeed, maxSpeed, minSpeed);

        if (prevSpeed == currentSpeed)
            return;

        for (int i = 0; i < spawnHandlers.Count; i++)
        {
            var spawnHandler = spawnHandlers[i];
            var timerData = Timer.Instance.GetTimer(x => x.id == spawnHandler.name);
            Timer.Instance.RemoveTimer(x => x.id == timerData.id);

            spawnHandler.SpawnPrevInterval = spawnHandler.SpawnInterval;
            spawnHandler.SpawnInterval = Mathf.Clamp(spawnHandler.SpawnInterval - speedMultipler * spawnHandler.SpawnInterval,
                                                     spawnHandler.SpawnMinInterval,
                                                     spawnHandler.SpawnMaxInterval);
            var timer = new TimerData()
            {
                id = timerData.id,
                currentTime = timerData.currentTime * timerData.waitTime / spawnHandler.SpawnInterval,
                waitTime = spawnHandler.SpawnInterval,
                fullTime = timerData.fullTime,
                action = () => SpawnMovementObject(spawnHandler)
            };
            Timer.Instance.AddTimer(timer);

            for (int j = 0; j < spawnHandler.SpawnedObjects.Count; j++)
            {
                var spawnedObject = spawnHandler.SpawnedObjects[j];
                spawnedObject.SpeedProxy.Value = new SpeedComponentData() { value = currentSpeed };
            }
        }
    }

    public void Run(SessionMovementObjectsOption option)
    {
        minSpeed = -option.MinSpeed;
        maxSpeed = -option.MaxSpeed;
        speedMultipler = option.SpeedMultiplier;
        currentSpeed = minSpeed;
        spawnHandlers.Clear();
        
        for (int i = 0; i < option.SpawnHandlers.Count; i++)
        {
            var spawnHandler = option.SpawnHandlers[i];
            if (spawnHandler.Filter.Count == 0)
                continue;

            spawnHandler.SpawnInterval = spawnHandler.SpawnMaxInterval;
            var timer = new TimerData()
            {
                id = spawnHandler.name,
                currentTime = 0,
                waitTime = spawnHandler.SpawnInterval,
                fullTime = 0,
                action = () => SpawnMovementObject(spawnHandler)
            };
            spawnHandlers.Add(spawnHandler);
            Timer.Instance.AddTimer(timer);
        }
        IsStopped = false;
        ObjectsReleased = 0;
    }

    public void Continue()
    {
        currentSpeed = prevSpeed == 0 ? minSpeed : prevSpeed;
        for (int i = 0; i < spawnHandlers.Count; i++)
        {
            var spawnHandler = spawnHandlers[i];
            if (spawnHandler.Filter.Count == 0)
                continue;

            Timer.Instance.RemoveTimer(x => x.id == spawnHandler.name);
            spawnHandler.SpawnInterval = spawnHandler.SpawnPrevInterval == 0 ? spawnHandler.SpawnMaxInterval : spawnHandler.SpawnPrevInterval;
            var timer = new TimerData()
            {
                id = spawnHandler.name,
                currentTime = 0,
                waitTime = spawnHandler.SpawnInterval,
                fullTime = 0,
                action = () => SpawnMovementObject(spawnHandler)
            };
            Timer.Instance.AddTimer(timer);
        }
    }

    public void Stop()
    {
        IsStopped = true;
        for(int i = 0; i < spawnHandlers.Count; i++)
        {
            var spawnHandler = spawnHandlers[i];
            Timer.Instance.RemoveTimer(x=>x.id == spawnHandler.name);
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

        for (int i = 0; i < spawnHandlers.Count; i++)
        {
            var spawnHelper = spawnHandlers[i];
            for (int j = spawnHelper.SpawnedObjects.Count - 1; j >= 0; j--)
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

                    ObjectsReleased++;
                    if (ObjectsReleased % releaseObjectsSpeedUpCount == 0)
                    {
                        SpeedUp();
                    }
                }
            }
        }
    }
}
