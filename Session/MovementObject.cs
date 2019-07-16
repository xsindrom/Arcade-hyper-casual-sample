using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Transforms;
public class MovementObject : MonoBehaviour, IPoolObject
{
    [SerializeField]
    private string id;
    [SerializeField]
    private SpeedComponentDataProxy speedProxy;
    [SerializeField]
    private PositionProxy positionProxy;
    [SerializeField]
    private List<Transform> parts = new List<Transform>();

    private Vector3 startPosition = Vector3.zero;
    private Vector3 releasePosition = Vector3.zero;
    private Vector3 passPosition = Vector3.zero;
    private bool wasPassed;

    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public Vector3 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }
    
    public Vector3 ReleasePosition
    {
        get { return releasePosition; }
        set { releasePosition = value; }
    }
    
    public Vector3 PassPosition
    {
        get { return passPosition; }
        set { passPosition = value; }
    }
    
    public SpeedComponentDataProxy SpeedProxy
    {
        get { return speedProxy; }
    }

    public PositionProxy PositionProxy
    {
        get { return positionProxy; }
    }

    public virtual void ResetObject()
    {
        transform.position = startPosition;
        positionProxy.Value = new Position() { Value = startPosition };

        wasPassed = false;
        for(int i = 0; i < parts.Count; i++)
        {
            var part = parts[i];
            part.gameObject.SetActive(true);
        }
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