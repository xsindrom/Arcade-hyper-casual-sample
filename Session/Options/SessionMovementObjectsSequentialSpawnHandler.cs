using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Session/Options/SessionMovementObjectsSequentialSpawnHandler")]
    public class SessionMovementObjectsSequentialSpawnHandler : SessionMovementObjectsSpawnHandler
    {
        [NonSerialized]
        protected int lastIndex;
        public int LastIndex
        {
            get { return lastIndex; }
        }

        public override string GenerateId()
        {
            if (lastIndex >= filter.Count)
                lastIndex = 0;

            var generated = filter[lastIndex];
            lastIndex++;
            return generated;
        }
    }
}