using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObject : MonoBehaviour, IPoolObject
{
    [SerializeField]
    private string id;
    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    [SerializeField]
    private Vector3 startPosition;
    public Vector3 StartPosition
    {
        get { return startPosition; }
    }
    [SerializeField]
    private Vector3 releasePosition;
    public Vector3 ReleasePosition
    {
        get { return releasePosition; }
    }
    [SerializeField]
    private Vector3 passPosition;
    public Vector3 PassPosition
    {
        get { return passPosition; }
    }
    [SerializeField]
    private SpeedComponentDataProxy speedProxy;
    public SpeedComponentDataProxy SpeedProxy
    {
        get { return speedProxy; }
    }

    private bool wasPassed;

    public virtual void ResetObject()
    {
        transform.position = startPosition;
        wasPassed = false;
    }

    public virtual bool CanBeReleased()
    {
        return transform.position.z < releasePosition.z;
    }

    public virtual bool CanPass()
    {
        return !wasPassed && transform.position.z < passPosition.z;
    }

    public virtual void Pass()
    {
        wasPassed = true;
    }
}