using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTabGroup : TabGroup
{
    [SerializeField]
    protected RectTransform toggleRoot;
    [SerializeField]
    protected AdvancedToggle template;
    [SerializeField]
    protected List<AdvancedToggle> toggles = new List<AdvancedToggle>();

    public override void Init()
    {
        base.Init();
        for (int i = 0; i < tabs.Count; i++)
        {
            var tab = tabs[i];
            if (i >= toggles.Count)
            {
                AddToggle(tab);
            }
            else
            {
                var index = i;
                toggles[index].Toggle.onValueChanged.AddListener(state =>
                {
                    if (state && !tabs[index].IsOpened)
                        OpenTab(index);
                });
            }
        }
    }

    public override void OpenTab(int index)
    {
        base.OpenTab(index);
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].Toggle.isOn = index == i;
            toggles[i].Toggle.interactable = tabs[i].IsAvailable;
        }
    }

    public override void OpenTab(string id)
    {
        base.OpenTab(id);
        for (int i = 0; i < tabs.Count; i++)
        {
            toggles[i].Toggle.isOn = tabs[i].Id == id;
            toggles[i].Toggle.interactable = tabs[i].IsAvailable;
        }
    }

    public override void UpdateTabs()
    {
        base.UpdateTabs();
        for (int i = 0; i < tabs.Count; i++)
        {
            toggles[i].Toggle.interactable = tabs[i].IsAvailable;
        }
    }

    public override void AddTab(ITab tab)
    {
        base.AddTab(tab);
        AddToggle(tab);
    }

    public override void RemoveTab(ITab tab)
    {
        RemoveToggle(tab);
        base.RemoveTab(tab);
    }

    public AdvancedToggle GetToggleByTab(ITab tab)
    {
        var index = tabs.IndexOf(tab);
        return index != -1 ? toggles[index] : null;
    }

    protected virtual void AddToggle(ITab tab)
    {
        var tabIndex = tabs.IndexOf(tab);
        if (tabIndex != -1)
        {
            var cloned = Instantiate(template, toggleRoot);
            cloned.gameObject.SetActive(true);
            cloned.BackgroundText.text = tab.Id;
            cloned.CheckmarkText.text = tab.Id;
            toggles.Add(cloned);

            cloned.Toggle.onValueChanged.AddListener(state =>
            {
                if (state)
                    OpenTab(tabIndex);
            });
            cloned.Toggle.interactable = tab.IsAvailable;
        }
    }

    protected virtual void RemoveToggle(ITab tab)
    {
        var tabIndex = tabs.IndexOf(tab);
        if (tabIndex != -1)
        {
            var toggle = toggles[tabIndex];
            if (toggle)
            {
                toggle.Toggle.onValueChanged.RemoveAllListeners();
                Destroy(toggle.gameObject);
            }
            toggles.RemoveAt(tabIndex);
        }
    }
}