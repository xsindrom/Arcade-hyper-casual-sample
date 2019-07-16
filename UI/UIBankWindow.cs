using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBankWindow : UIBaseWindow
{
    [SerializeField]
    private List<UIBankSection> sections = new List<UIBankSection>();

    public override void Init()
    {
        base.Init();

        for (int i = 0; i < sections.Count; i++)
        {
            var section = sections[i];
            section.Init();
        }
    }
}
