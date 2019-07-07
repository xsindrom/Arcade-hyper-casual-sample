using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdvancedToggle : MonoBehaviour
{
    [SerializeField]
    protected Toggle toggle;
    [SerializeField]
    protected TMP_Text backgroundText;
    [SerializeField]
    protected TMP_Text checkmarkText;

    public Toggle Toggle
    {
        get { return toggle; }
        set { toggle = value; }
    }

    public TMP_Text BackgroundText
    {
        get { return backgroundText; }
        set { backgroundText = value; }
    }

    public TMP_Text CheckmarkText
    {
        get { return checkmarkText; }
        set { checkmarkText = value; }
    }
}