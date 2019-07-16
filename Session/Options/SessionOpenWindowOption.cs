using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    public abstract class SessionOpenWindowOption : SessionOption
    {
        [SerializeField]
        protected string windowId;
        public string WindowId
        {
            get { return windowId; }
        }
    }
}