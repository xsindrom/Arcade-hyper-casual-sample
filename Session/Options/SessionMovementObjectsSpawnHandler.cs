using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    public abstract class SessionMovementObjectsSpawnHandler : ScriptableObject
    {
        [SerializeField]
        protected int spawnMaxCount;
        [SerializeField]
        protected float spawnInterval;
        [SerializeField]
        protected List<string> filter = new List<string>();

        [NonSerialized]
        protected int spawnedCount;
        [NonSerialized]
        protected TimerData spawnTimerData;
        [NonSerialized]
        protected List<MovementObject> spawnedObjects = new List<MovementObject>();

        public int SpawnMaxCount
        {
            get { return spawnMaxCount; }
        }

        public float SpawnInterval
        {
            get { return spawnInterval; }
        }

        public List<string> Filter
        {
            get { return filter; }
        }

        public int SpawnedCount
        {
            get { return spawnedCount; }
            set { spawnedCount = value; }
        }

        public TimerData SpawnTimerData
        {
            get { return spawnTimerData; }
            set { spawnTimerData = value; }
        }

        public List<MovementObject> SpawnedObjects
        {
            get { return spawnedObjects; }
        }

        public abstract string GenerateId();
    }
}