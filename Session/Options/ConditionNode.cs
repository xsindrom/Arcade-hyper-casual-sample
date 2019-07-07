using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session
{
    public abstract class ConditionNode : ScriptableObject
    {
        public abstract bool IsValid();
    }
}