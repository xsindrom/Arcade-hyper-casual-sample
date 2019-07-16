using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Session/Options/SessionStartOpenWindowOption")]
    public class SessionStartOpenWindowOption : SessionOpenWindowOption
    {
        public override void StartSession()
        {
            base.StartSession();
            var window = UIMainController.Instance.GetWindow(WindowId);
            window?.OpenWindow();
        }
    }
}