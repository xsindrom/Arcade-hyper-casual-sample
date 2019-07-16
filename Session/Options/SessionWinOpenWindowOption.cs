using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Session/Options/SessionWinOpenWindowOption")]
    public class SessionWinOpenWindowOption : SessionOpenWindowOption
    {
        public override void WinSession()
        {
            base.WinSession();
            var window = UIMainController.Instance.GetWindow(WindowId);
            window?.OpenWindow();
        }
    }
}