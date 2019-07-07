using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Session/Options/SessionMovementObjectsOption")]
    public class SessionMovementObjectsOption : SessionOption
    {
        [SerializeField]
        private float minSpeed = 1;
        [SerializeField]
        private float maxSpeed = 2;
        [SerializeField]
        private float speedMultiplier = 1.0f;
        [SerializeField]
        private List<SessionMovementObjectsSpawnHandler> spawnHandlers = new List<SessionMovementObjectsSpawnHandler>();

        public float MinSpeed
        {
            get { return minSpeed; }
        }

        public float MaxSpeed
        {
            get { return maxSpeed; }
        }

        public float SpeedMultiplier
        {
            get { return speedMultiplier; }
        }

        public List<SessionMovementObjectsSpawnHandler> SpawnHandlers
        {
            get { return spawnHandlers; }
        }

        public override void StartSession()
        {
            SessionController.Instance.MovementObjectsController.Run(this);
        }
    }
}