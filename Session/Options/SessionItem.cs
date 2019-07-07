using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Session/Options/SessionItem", order = 1)]
    public class SessionItem : ScriptableObject
    {
        [SerializeField]
        private List<SessionOption> options = new List<SessionOption>();

        public List<SessionOption> Options
        {
            get { return options; }
        }

        public void StartSession()
        {
            for(int i = 0; i < options.Count; i++)
            {
                options[i].StartSession();
            }
        }

        public void WinSession()
        {
            for (int i = 0; i < options.Count; i++)
            {
                options[i].WinSession();
            }
        }

        public void LoseSession()
        {
            for (int i = 0; i < options.Count; i++)
            {
                options[i].LoseSession();
            }
        }

    }
}