using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Session/Options/SessionMovementObjectsRandomSpawnHandler")]
    public class SessionMovementObjectsRandomSpawnHandler : SessionMovementObjectsSpawnHandler
    {
        public override string GenerateId()
        {
            return Filter[Random.Range(0, Filter.Count)];
        }
    }
}