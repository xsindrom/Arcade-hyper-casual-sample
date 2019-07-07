using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField]
    protected RectTransform tabsRoot;
    public RectTransform TabsRoot
    {
        get { return tabsRoot; }
    }

    protected List<ITab> tabs = new List<ITab>();

    public event Action<ITab> OnTabAdded;
    public event Action<ITab> OnTabRemoved;
    public event Action<ITab> OnTabOpened;
    public event Action<ITab> OnTabClosed;

    public virtual void Init()
    {
        for (int i = 0; i < tabsRoot.childCount; i++)
        {
            var child = tabsRoot.GetChild(i);
            var tab = child.GetComponent<ITab>();
            if (tab != null)
            {
                tabs.Add(tab);
            }
        }

        for (int i = 0; i < tabs.Count; i++)
        {
            tabs[i].ParentGroup = this;
            tabs[i].InitTab();
        }
    }

    public virtual void OpenTab(string id)
    {
        var tab = GetTab(id);
        if (tab != null)
        {
            for (int i = 0; i < tabs.Count; i++)
            {
                var iTab = tabs[i];
                if (iTab.Id != id)
                {
                    CloseTab(i);
                }
            }
            tab.OpenTab();
            tab.IsOpened = true;
            OnTabOpened?.Invoke(tab);
        }
    }

    public virtual void OpenTab(int index)
    {
        var tab = GetTab(index);
        if (tab != null)
        {
            for (int i = 0; i < tabs.Count; i++)
            {
                if (index != i)
                {
                    CloseTab(i);
                }
            }
            tab.OpenTab();
            tab.IsOpened = true;
            OnTabOpened?.Invoke(tab);
        }
    }

    public virtual void CloseTab(string id)
    {
        var tab = GetTab(id);
        if (tab != null)
        {
            tab.CloseTab();
            tab.IsOpened = false;
            OnTabClosed?.Invoke(tab);
        }
    }

    public virtual void CloseTab(int index)
    {
        var tab = GetTab(index);
        if (tab != null)
        {
            tab.CloseTab();
            tab.IsOpened = false;
            OnTabClosed?.Invoke(tab);
        }
    }

    public virtual void UpdateTabs()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            var tab = tabs[i];
            tab.UpdateTab();
        }
    }

    public virtual void CloseAll()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            CloseTab(i);
        }
    }

    public ITab GetTab(string id)
    {
        return tabs.Find(x => x.Id == id);
    }

    public ITab GetTab(int index)
    {
        return 0 > index || index >= tabs.Count ? null : tabs[index];
    }

    public ITab GetCurrentOpenedTab(out int index)
    {
        index = tabs.FindIndex(x => x.IsOpened);
        return index != -1 ? tabs[index] : null;
    }

    public int IndexOf(ITab tab)
    {
        return tabs.IndexOf(tab);
    }

    public virtual void AddTab(ITab tab)
    {
        if (!tabs.Contains(tab))
        {
            tabs.Add(tab);
            OnTabAdded?.Invoke(tab);
        }
    }

    public virtual void RemoveTab(ITab tab)
    {
        if (tabs.Contains(tab))
        {
            tabs.Remove(tab);
            OnTabRemoved?.Invoke(tab);
        }
    }

    public bool IsTabOpened(int index)
    {
        var tab = GetTab(index);
        return tab != null ? tab.IsOpened : false;
    }

    public bool IsTabOpened(string id)
    {
        var tab = GetTab(id);
        return tab != null ? tab.IsOpened : false;
    }
}