using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Session;
using Session.Options;

public class UIMissionsWindow : UIBaseWindow, ISessionHandler
{
    [SerializeField]
    private UISessionItem uiSessionSettingsItemTemplate;
    [SerializeField]
    private RectTransform uiSessionSettingsItemsRoot;
    private List<UISessionItem> uiSessionSettingsItems = new List<UISessionItem>();

    public override void Init()
    {
        base.Init();

        var sessionStorage = SessionController.Instance.SessionItemsStorage;
        for (int i = 0; i < sessionStorage.Sessions.Count; i++)
        {
            var sessionSettings = sessionStorage.Sessions[i];
            var cloned = Instantiate(uiSessionSettingsItemTemplate, uiSessionSettingsItemsRoot);
            cloned.Id = sessionSettings.Id;
            cloned.Source = sessionSettings;
            uiSessionSettingsItems.Add(cloned);
        }
        EventManager.AddHandler(this);
    }

    private void OnDestroy()
    {
        EventManager.RemoveHandler(this);
    }

    public void StartSession()
    {
        CloseWindow();
    }

    public void LoseSession()
    {

    }

    public void WinSession()
    {

    }

    public override void OpenWindow()
    {
        for(int i = 0; i < uiSessionSettingsItems.Count; i++)
        {
            uiSessionSettingsItems[i].UpdateItem();
        }
        base.OpenWindow();
    }
}
