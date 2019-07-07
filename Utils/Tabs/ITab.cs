using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ITab
{
    string Id { get; set; }
    bool IsAvailable { get; }
    bool IsOpened { get; set; }
    TabGroup ParentGroup { get; set; }
    void UpdateTab();
    void InitTab();
    void OpenTab();
    void CloseTab();
}