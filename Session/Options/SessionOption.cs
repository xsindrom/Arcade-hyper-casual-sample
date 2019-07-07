using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    public abstract class SessionOption : ScriptableObject
    {
        public virtual void StartSession()
        {

        }

        public virtual void WinSession()
        {

        }

        public virtual void LoseSession()
        {

        }

    }
}