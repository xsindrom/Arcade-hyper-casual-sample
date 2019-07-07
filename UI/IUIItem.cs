using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIItem<T>
{
    string Id { get; set; }
    T Source { get; set; }
}
