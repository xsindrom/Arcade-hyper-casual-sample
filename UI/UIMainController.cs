using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainController : MonoSingleton<UIMainController>
{
    [SerializeField]
    private RectTransform windowsRoot;
    public RectTransform WindowsRoot
    {
        get { return windowsRoot; }
    }
    [SerializeField]
    private List<UIBaseWindow> windows = new List<UIBaseWindow>();
    private Dictionary<string, UIBaseWindow> windowsDict = new Dictionary<string, UIBaseWindow>();

    public event Action OnWindowsLoaded;
    public event Action<UIBaseWindow> OnWindowAdded;
    public event Action<string> OnWindowRemoved;

    public override void Init()
    {
        base.Init();
        for (int i = 0; i < windows.Count; i++)
        {
            var cloned = Instantiate(windows[i], windowsRoot);
            windowsDict[cloned.Id] = cloned;
            cloned.gameObject.SetActive(cloned.WindowState == WindowState.Opened);
            cloned.Init();
        }
        OnWindowsLoaded?.Invoke();
    }

    public T GetWindow<T>(string windowId) where T: UIBaseWindow
    {
        return windowsDict.ContainsKey(windowId) ? windowsDict[windowId] as T : null;
    }

    public UIBaseWindow GetWindow(string windowId)
    {
        return windowsDict.ContainsKey(windowId) ? windowsDict[windowId] : null;
    }

    public void AddWindow(UIBaseWindow window)
    {
        if (!window)
            return;

        if (!windowsDict.ContainsKey(window.Id))
        {
            windowsDict[window.Id] = Instantiate(window, windowsRoot);
            OnWindowAdded?.Invoke(window);
        }
    }

    public void RemoveWindow(UIBaseWindow window)
    {
        if (!window)
            return;

        var windowId = window.Id;
        windowsDict.Remove(windowId);
        OnWindowRemoved?.Invoke(windowId);
        Destroy(window.gameObject);
    }

}
