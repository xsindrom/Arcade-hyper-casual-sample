using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance = null;
    private static object lockObject = new object();

    private static bool isApplicationQuiting = false;

    public static T Instance
    {
        get
        {
            if (isApplicationQuiting)
                return null;

            if (!instance)
            {
                lock (lockObject)
                {
                    var instances = FindObjectsOfType<T>();
                    if (instances.Length > 0)
                    {
                        instance = instances[0];
                        for (int i = 1; i < instances.Length; i++)
                        {
                            Destroy(instances[i].gameObject);
                        }
                    }
                    if (!instance)
                    {
                        var newInstanceObject = new GameObject();
                        instance = newInstanceObject.AddComponent<T>();
                        newInstanceObject.name = typeof(T).Name;
                    }
                }
            }
            return instance;
        }
    }

    public static bool HasInstance
    {
        get { return instance; }
    }

    [SerializeField]
    protected bool dontDestroyOnLoad = false;

    private void Awake()
    {
        if (Instance == this)
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            Init();
        }
    }

    public static void CreateInstance(bool dontDestroyOnLoad = false)
    {
        if (!instance)
        {
            var gameObject = new GameObject();
            instance = gameObject.AddComponent<T>();
            gameObject.name = typeof(T).Name;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    public virtual void Init()
    {

    }

    protected virtual void OnApplicationPause(bool pause)
    {

    }

    protected virtual void OnApplicationQuit()
    {
        isApplicationQuiting = true;
    }

    protected virtual void OnDestroy()
    {

    }
}
