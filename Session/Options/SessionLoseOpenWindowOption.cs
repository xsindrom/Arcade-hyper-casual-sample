using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Session/Options/SessionLoseOpenWindowOption")]
    public class SessionLoseOpenWindowOption : SessionOpenWindowOption
    {
        public override void LoseSession()
        {
            base.LoseSession();
            var window = UIMainController.Instance.GetWindow(WindowId);
            window?.OpenWindow();
        }
    }
}