using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WindowState { Opened, Closed }

public class UIBaseWindow : MonoBehaviour
{
    [SerializeField]
    protected string id;
    public virtual string Id
    {
        get { return id; }
        set { id = value; }
    }

    [SerializeField]
    protected WindowState windowState;
    public WindowState WindowState
    {
        get { return windowState; }
        set
        {
            if (windowState != value)
            {
                var prevState = windowState;
                windowState = value;
                OnWindowStateChanged?.Invoke(prevState, windowState);
            }
        }
    }
    public event Action<WindowState, WindowState> OnWindowStateChanged;

    public virtual void Init()
    {

    }

    public virtual void OpenWindow()
    {
        gameObject.SetActive(true);
        WindowState = WindowState.Opened;
    }

    public virtual void CloseWindow()
    {
        gameObject.SetActive(false);
        WindowState = WindowState.Closed;
    }
}
