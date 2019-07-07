using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITab : MonoBehaviour, ITab
{
    [SerializeField]
    protected string id;

    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public bool IsAvailable
    {
        get { return true; }
    }

    public bool IsOpened { get; set; }
    public TabGroup ParentGroup { get; set; }

    public virtual void InitTab()
    {
    }

    public virtual void OpenTab()
    {
    }

    public virtual void CloseTab()
    {

    }

    public virtual void UpdateTab()
    {
    }
}
