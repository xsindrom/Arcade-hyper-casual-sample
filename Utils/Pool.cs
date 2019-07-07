using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolObject
{
    string Id { get; set; }
    void ResetObject();
}

public class Pool<T> : MonoBehaviour where T: MonoBehaviour, IPoolObject
{
    [SerializeField]
    private Transform objectsRoot;
    [SerializeField]
    private List<T> templates = new List<T>();

    private List<T> freeObjects = new List<T>();
    private List<T> busyObjects = new List<T>();

    public T GetOrInstantiate(string id)
    {
        var free = freeObjects.FindAll(x => x.Id == id);
        if(free.Count > 0)
        {
            var freeObject = free[0];
            freeObject.ResetObject();
            freeObjects.Remove(freeObject);
            freeObject.gameObject.SetActive(true);
            busyObjects.Add(freeObject);
            return freeObject;
        }
        else
        {
            var template = templates.Find(x => x.Id == id);
            if (!template)
                return null;

            var cloned = Instantiate(template, objectsRoot);
            cloned.ResetObject();
            cloned.gameObject.SetActive(true);
            busyObjects.Add(cloned);
            return cloned;
        }
    }

    public void ReleaseObject(T releaseObject)
    {
        if (!releaseObject)
            return;

        busyObjects.Remove(releaseObject);
        releaseObject.ResetObject();
        releaseObject.gameObject.SetActive(false);
        freeObjects.Add(releaseObject);
    }

    public void ReleaseAll()
    {
        for(int i = 0; i < busyObjects.Count; i++)
        {
            var busyObject = busyObjects[i];
            busyObject.ResetObject();
            busyObject.gameObject.SetActive(false);
            freeObjects.Add(busyObject);
        }
        busyObjects.Clear();
    }
}
